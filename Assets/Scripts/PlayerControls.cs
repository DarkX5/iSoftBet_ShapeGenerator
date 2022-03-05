using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    // // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        if (Input.GetMouseButton(0))
        {
            CheckHit();
        }
    }

    private void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            hit.collider.transform.GetComponent<FallingShape>().Die();
        }

        // Ray2D ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit2D hit;
        // bool hasHit = Physics2D.Raycast(ray, out hit); //, enemyLayer);
        // if (hasHit)
        // {
        //     hit.transform.GetComponent<FallingShape>().Die();
        //     // navAgent.destination = hit.point;
        // Debug.Log("Target name: " + hit.collider.name);
        // }
    }
}
