using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShape : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Floor")
        {
            // Init(new Vector3(Random.Range(-10f, 10f), 5f, transform.position.z), spriteRenderer.material);
            gameObject.SetActive(false);
        }
    }

    public void Init(Vector3 newPosition, Material newMaterial) {
        transform.position = newPosition;
        spriteRenderer.material = newMaterial;
    }
}
