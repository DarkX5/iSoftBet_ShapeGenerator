using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance { get; private set; }
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int shapesHit = 0;

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
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider != null) {
            hit.collider.transform.GetComponent<FallingShape>()?.Hide();
            shapesHit += 1;
        }
        else {
            ShapeGenerator.Instance
            .SpawnNewShapeAtPosition(new Vector3(Input.mousePosition.x, 
                                                    Input.mousePosition.y, 2f));
        }
    }
}
