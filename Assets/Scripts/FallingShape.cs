using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShape : MonoBehaviour
{
    [SerializeField] private float surfaceAreaSizePixels = 0;
    private SpriteRenderer[] spriteRenderers = null;
    private Rigidbody2D attachedRigidbody;
    Color newMaterialColor;
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

    public float SurfaceAreaSizePixels { get { return surfaceAreaSizePixels; } }
    // private void OnEnable() {
    //     ShapeGenerator.Instance.onGravityChangeAction += Init;
    //     ShapeGenerator.Instance.inactiveShapesList -= Init;
    // }
    // private void OnDisable()
    // {
    //     ShapeGenerator.Instance.onGravityChangeAction -= Init;
    //     ShapeGenerator.Instance.inactiveShapesList += Init;
    // }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer != gameObject.layer)
        {
            Hide();
        }
    }

    public void Init()
    {
        // gameObject.SetActive(true);
        if (spriteRenderers == null)
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        GetSize();
        Reset();
    }
    public void InitAtPosition(Vector3 newPosition) {
        // gameObject.SetActive(true);
        if (spriteRenderers == null)
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        GetSize();
        ResetAtPosition(newPosition);
    }

    public void Reset() {
        if (gameObject.activeSelf)
            return;
        gameObject.SetActive(true);
        transform.localPosition = Camera.main.ViewportToWorldPoint(RandomPosition);
        newMaterialColor = RandomColor;
        foreach (var sRenderer in spriteRenderers)
        {
            sRenderer.transform.localRotation = new Quaternion( 0f,
                                                                0f,
                                                                Random.Range(-1f, 1f),
                                                                sRenderer.transform.localRotation.w);
            // sRenderer.transform.localScale = new Vector3(Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f));
            sRenderer.material.color = newMaterialColor;
        }
    }
    public void ResetAtPosition(Vector3 newPosition)
    {
        if (gameObject.activeSelf)
            return;
        gameObject.SetActive(true);
        transform.localPosition = Camera.main.ScreenToWorldPoint(newPosition);
        newMaterialColor = RandomColor;
        foreach (var sRenderer in spriteRenderers)
        {
            sRenderer.transform.localRotation = new Quaternion( 0f,
                                                                0f,
                                                                Random.Range(-1f, 1f),
                                                                sRenderer.transform.localRotation.w);
            // sRenderer.transform.localScale = new Vector3(Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f));
            sRenderer.material.color = newMaterialColor;
        }
    }

    public void SetGravity(float newGravity) {
        if (attachedRigidbody == null)
            attachedRigidbody = GetComponent<Rigidbody2D>();
        attachedRigidbody.gravityScale = newGravity;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void GetSize() {
        if (surfaceAreaSizePixels != 0)
            return;

        Vector3 bounds = spriteRenderers[0].bounds.center;
        surfaceAreaSizePixels = Mathf.Abs(bounds.x * bounds.y);
        
        // //get world space size (this version handles rotating correctly)
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
}
