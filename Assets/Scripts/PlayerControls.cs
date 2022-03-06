using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance { get; private set; }
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private int shapesHit = 0;

    private AudioSource audioSource;

    public int HitsNo { get { return shapesHit; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start() {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
            
        audioSource.clip = hitSound;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
        }
    }

    private void CheckHit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider != null) {
            hit.collider.transform.GetComponent<FallingShape>()?.Hide();
            audioSource.Play();
            shapesHit += 1;
        }
        else {
            ShapeGenerator.Instance
            .SpawnNewShapeAtPosition(new Vector3(Input.mousePosition.x, 
                                                    Input.mousePosition.y, 2f));
        }
    }
}
