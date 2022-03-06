using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShape : MonoBehaviour
{
    [SerializeField] private float surfaceAreaSizePixels = 0;
    [SerializeField] private SpriteRenderer[] spriteRenderers = null;
    private Rigidbody2D attachedRigidbody = null;
    private Color newMaterialColor;
    private AudioSource audioSource = null;
    private Color RandomColor
    {
        get
        {
            return new Color(Random.Range(0f, 1f),
                             Random.Range(0f, 1f),
                             Random.Range(0f, 1f), 0f);
        }
    }
    private Vector3 RandomPosition
    {
        get
        {
            return new Vector3(Random.Range(0.05f, 0.95f),
                               1.0f,
                               1.5f);
        }
    }
    private Vector3 RandomScale
    {
        get
        {
            return new Vector3( Random.Range(0.35f, 2f),
                                Random.Range(0.35f, 2f),
                                Random.Range(0.35f, 2f));
        }
    }

    public float SurfaceAreaSizePixels { get { return surfaceAreaSizePixels; } }

    // private void OnEnable() {
    //     ShapeGenerator.Instance.activeShapesList += SetGravity;
    //     ShapeGenerator.Instance.inactiveShapesList -= SetGravity;
    // }
    // private void OnDisable()
    // {
    //     ShapeGenerator.Instance.activeShapesList -= SetGravity;
    //     ShapeGenerator.Instance.inactiveShapesList += SetGravity;
    // }
    private void Awake() {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }
        if (spriteRenderers == null || spriteRenderers.Length == 0)
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (surfaceAreaSizePixels == 0)
            InitSize();

        MenuActions.Instance.onGravityIncreaseAction += SetGravity;
        MenuActions.Instance.onGravityDecreaseAction += SetGravity;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer != gameObject.layer)
        {
            Hide();
        }
    }
    private void OnEnable()
    {
        // get new color for init / reset
        newMaterialColor = RandomColor;

        audioSource.clip = ShapeGenerator.Instance.RandomCreationClip;
        audioSource?.Play();
    }

    public void Init() {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        else
            return;

        SetGravity();
        Reset();
    }
    public void InitAtPosition(Vector3 newPosition) {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        else
            return;

        SetGravity();
        ResetAtPosition(newPosition);
    }

    public void Reset() {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        transform.localPosition = Camera.main.ViewportToWorldPoint(RandomPosition);
        SetMaterialColor();
    }
    public void ResetAtPosition(Vector3 newPosition)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        transform.localPosition = Camera.main.ScreenToWorldPoint(newPosition);
        SetMaterialColor();
    }

    public void SetGravity() { //float newGravity) {
        if (attachedRigidbody == null)
            attachedRigidbody = GetComponent<Rigidbody2D>();
        // attachedRigidbody.gravityScale = newGravity;
        attachedRigidbody.gravityScale = ShapeGenerator.Instance.Gravity;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void InitSize() {
        Vector3 bounds = spriteRenderers[0].bounds.center;
        surfaceAreaSizePixels = Mathf.Abs(bounds.x * bounds.y);

        // //get world space size - way 2 much of a performance hit for an exact size (without setting the size manually on this script)...
        // Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        // Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        // Vector3 world_size = local_sprite_size;
        // world_size.x *= transform.lossyScale.x;
        // world_size.y *= transform.lossyScale.y;

        // //convert to screen space size
        // Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
        // screen_size.y *= Camera.main.aspect;

        // //size in pixels
        // Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;

        // surfaceAreaSizePixels = in_pixels.x * in_pixels.y;
        // // Debug.Log(string.Format("World size: {0}, Screen size: {1}, Pixel size: {2}", world_size, screen_size, in_pixels));
    }

    private void SetMaterialColor()
    {
        foreach (var sRenderer in spriteRenderers)
        {
            if (Random.Range(0, 101) > 50) {
                sRenderer.transform.localRotation = new Quaternion( 0f, 0f, Random.Range(-1f, 1f),
                                                                    sRenderer.transform.localRotation.w);

                // use ununiform  scale to generate a unique shape from the base sprite
                sRenderer.transform.localScale = RandomScale;
            } else {
                sRenderer.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
                // use default object scale
                sRenderer.transform.localScale = Vector3.one;
            }
            sRenderer.material.color = newMaterialColor;
        }
    }
}
