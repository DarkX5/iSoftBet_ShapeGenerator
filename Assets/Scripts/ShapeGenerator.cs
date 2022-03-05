using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private FallingShape[] spawnedShapes = null;
    // [SerializeField] private Material[] spawnedMaterials = null;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private float spawnTimeSeconds = 0.1f;
    [SerializeField] private int shapesNoPerSecond = 2;

    int idx;
    Color newMaterialColor;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnedShapes == null || spawnedShapes.Length < 1)
            spawnedShapes = GetComponentsInChildren<FallingShape>();
        Timing.RunCoroutine(SpawnNewShapesCo());
    }

    IEnumerator<float> SpawnNewShapesCo() {
        idx = 0;
        for(int i = 0; i < spawnedShapes.Length; i += 1) {
            if(spawnedShapes[i].gameObject.activeSelf == false) {
                spawnedShapes[i].gameObject.SetActive(true);
                spawnedShapes[i].Init(new Vector3(Random.Range(-10f, 10f), 5f, transform.position.z),
                                      new Color(Random.Range(0f, 1f),
                                                Random.Range(0f, 1f),
                                                Random.Range(0f, 1f), 0f));

                // avoid spawning all possible shapes at once - greater falling Y spread
                if(((int)(0.1 / spawnedShapes.Length) + 1) > idx)
                    break;
                else
                    idx += 1;
            }
        }
        yield return Timing.WaitForSeconds(spawnTimeSeconds);
        Timing.RunCoroutine(SpawnNewShapesCo());
    }
}
