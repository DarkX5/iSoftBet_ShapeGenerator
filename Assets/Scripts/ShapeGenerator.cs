using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private FallingShape[] spawnedShapes = null;
    [SerializeField] private Material[] spawnedMaterials = null;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private float spawnTimeSeconds = 0.1f;

    [SerializeField] private int shapesNoPerSecond = 2;

    int idx;

    // Start is called before the first frame update
    void Start()
    {
        spawnedShapes = GetComponentsInChildren<FallingShape>();
        StartCoroutine(SpawnNewShapesCo());
    }

    IEnumerator SpawnNewShapesCo() {
        idx = 0;
        for(int i = 0; i < spawnedShapes.Length; i += 1) {
            if(spawnedShapes[i].gameObject.activeSelf == false) {
                spawnedShapes[i].gameObject.SetActive(true);
                spawnedShapes[i].Init(new Vector3(Random.Range(-10f, 10f), 5f, transform.position.z), spawnedMaterials[Random.Range(0, spawnedMaterials.Length)]);
                if(((int)(0.1 / spawnedShapes.Length) + 1) > idx)
                    break;
                else
                    idx += 1;
            }
        }
        yield return new WaitForSeconds(spawnTimeSeconds);
        StartCoroutine(SpawnNewShapesCo());
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
