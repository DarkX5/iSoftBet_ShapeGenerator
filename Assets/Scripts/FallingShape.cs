using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShape : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    // Shader newMatShader;
    // Material newMat;
    // Color newColor;
    // private SpriteRenderer[] renderers;

    // int idx;
    private void Start() {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // newMat = new Material(newMatShader);
        // foreach(var renderer in spriteRenderers) {
        //     renderer.material = newMat;
        // }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Floor")
        {
            Die();
            // gameObject.SetActive(false);
            // Init(new Vector3(Random.Range(-10f, 10f), 5f, transform.position.z), spriteRenderer.material);
        }
    }

    public void Init(Vector3 newPosition, Color newMaterialColor) {
        transform.position = newPosition;
        foreach(var sRenderer in spriteRenderers) {
            // sRenderer.transform.localRotation = new Quaternion( Random.Range(0f, 30f),
            //                                                     Random.Range(0f, 30f),
            //                                                     Random.Range(0f, 360f),
            //                                                     sRenderer.transform.localRotation.w);
            sRenderer.transform.localRotation = new Quaternion( 0f,
                                                                0f,
                                                                Random.Range(0f, 360f),
                                                                sRenderer.transform.localRotation.w);
            // sRenderer.transform.localScale = new Vector3(Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f),
            //                                              Random.Range(0.25f, 3f));
            sRenderer.material.color = newMaterialColor;
            // sRenderer.material = newMaterial;
            // sRenderer.material.SetColor("_Color", newMaterialColor);
            // idx = sRenderer.material.shader.GetInstanceID();
            // sRenderer.material.SetColor(idx, newMaterialColor);
            // sRenderer.color = newMaterialColor;
            // newMat.SetColor(newMatShader.name, 
            //                 new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), 255f));
        }
        // spriteRenderer.material = newMaterial;
    }

    public void Die() {
        gameObject.SetActive(false);
    }
}
