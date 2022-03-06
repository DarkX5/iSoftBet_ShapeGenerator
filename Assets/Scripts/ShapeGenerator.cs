using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.Events;

public class ShapeGenerator : MonoBehaviour
{
    public static ShapeGenerator Instance { get; private set; }
    [SerializeField] private GameObject[] spawnableList = null;
    [SerializeField] private AudioClip[] shapeCreationSounds = null;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private float spawnFrequencySeconds = 1f; //0.1f;
    [SerializeField] private int shapesNoPerSecond = 1;
    [SerializeField] private int maxShapeInstances = 250;
    [SerializeField] private int gravity = 1;
    [SerializeField] [Range(0.001f, 1f)] private float gravityOffset = 0.2f;
    private List<FallingShape> spawnedShapes = new List<FallingShape>();
    private List<Rigidbody2D> spawnedShapeRigidbodies = new List<Rigidbody2D>();

    private int noOfCurrentShapes = 0;
    private float surfaceAreaOccupiedByShapes = 0f;
    private bool shapeFound;
    private Color newMaterialColor;
    private FallingShape newShape;
    private float screenWidth = 0f;
    private Rigidbody2D rigidBody;

    // remember last used shape type -> so we don't generate the same shape twice in a row
    private string lastShape;
    // private AudioSource audioSource = null;

    public AudioClip RandomCreationClip {
        get {
            return shapeCreationSounds[UnityEngine.Random.Range(0, shapeCreationSounds.Length)];
        }
    }

    public int Gravity { get { return gravity; } }
    public int ShapesNoPerSecond { get { return shapesNoPerSecond; } }
    public int NoOfCurrentShapes { get { return noOfCurrentShapes; } }
    public float SurfaceAreaOccupiedByShapes { get { return surfaceAreaOccupiedByShapes; } }

    // public event Action activeShapesList;
    // public event Action inactiveShapesList;

    // private Delegate[] tmpActiveShapes;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuActions.Instance.onGravityIncreaseAction += GravityIncrease;
        MenuActions.Instance.onGravityDecreaseAction += GravityDecrease;

        if(spawnedShapes == null || spawnedShapes.Count < 1) {
            foreach(var shape in GetComponentsInChildren<FallingShape>()) {
                spawnedShapes.Add(shape);
            };
        }

        Timing.RunCoroutine(SpawnNewShapesCo());
    }

    IEnumerator<float> SpawnNewShapesCo()
    {
        if(shapesNoPerSecond > 0) {
            SpawnNewShape();
            yield return Timing.WaitForSeconds(spawnFrequencySeconds / shapesNoPerSecond);
        }
        else {
            // avoid error with shape generation when 0
            noOfCurrentShapes = 0;
            foreach(FallingShape shape in spawnedShapes) {
                if(shape.gameObject.activeSelf == true) {
                    noOfCurrentShapes += 1;
                }
            }
            yield return Timing.WaitForSeconds(0.25f);
        }

        Timing.RunCoroutine(SpawnNewShapesCo());
    }

    private void SpawnNewShape()
    {
        /* faster - but prone to errors -> tmpInactiveShapes list doesn't update fast enough 
          -> can be fixed -> usefull ONLY if more performance NEEDED */
        // tmpActiveShapes = activeShapesList?.GetInvocationList();
        // noOfCurrentShapes = tmpActiveShapes != null ? tmpActiveShapes.Length : 0;
        // var tmpInctiveCount = inactiveShapesList?.GetInvocationList().Length ?? 0;
        // if(tmpInctiveCount > 0) {
        //     inactiveShapesList.GetInvocationList()[UnityEngine.Random.Range(0, tmpInctiveCount)].DynamicInvoke();
        // }

        // check for the first available shape
        noOfCurrentShapes = 0;
        surfaceAreaOccupiedByShapes = 0;
        shapeFound = false;
        for (int i = 0; i < spawnedShapes.Count; i += 1)
        {
            if (spawnedShapes[i].gameObject.activeSelf == true)
            {
                noOfCurrentShapes += 1;
                surfaceAreaOccupiedByShapes += spawnedShapes[i].SurfaceAreaSizePixels;
            } else if (!shapeFound && spawnedShapes[i].name != lastShape) {
                spawnedShapes[i].Reset();
                shapeFound = true;

                lastShape = spawnedShapes[i].name;
                noOfCurrentShapes += 1;
                surfaceAreaOccupiedByShapes += spawnedShapes[i].SurfaceAreaSizePixels;
                // break;
            }
        }

        // no shape available - instantiate new shape
        if (!shapeFound)
        {
            if (spawnedShapes.Count > maxShapeInstances)
                return;

            newShape = (Instantiate(spawnableList[UnityEngine.Random.Range(0, spawnableList.Length)], transform)).GetComponent<FallingShape>();
            newShape.Init();
            spawnedShapes.Add(newShape);
        }
    }
    public void SpawnNewShapeAtPosition(Vector3 newPosition)
    {
        noOfCurrentShapes = 0;
        surfaceAreaOccupiedByShapes = 0f;
        shapeFound = false;
        for (int i = 0; i < spawnedShapes.Count; i += 1)
        {
            if (spawnedShapes[i].gameObject.activeSelf == false)
            {
                if (!shapeFound && spawnedShapes[i].name != lastShape) {
                    spawnedShapes[i].ResetAtPosition(newPosition);
                    shapeFound = true;

                    lastShape = spawnedShapes[i].name;
                    noOfCurrentShapes += 1;
                    surfaceAreaOccupiedByShapes += spawnedShapes[i].SurfaceAreaSizePixels;
                }
            } else {
                noOfCurrentShapes += 1;
                surfaceAreaOccupiedByShapes += spawnedShapes[i].SurfaceAreaSizePixels;
            }
        }

        if (!shapeFound)
        {
            newShape = (Instantiate(spawnableList[UnityEngine.Random.Range(0, spawnableList.Length)], transform)).GetComponent<FallingShape>();
            newShape.InitAtPosition(newPosition);
            spawnedShapes.Add(newShape);
        }
    }

    public void ShapesPerSecondIncrease()
    {
        if (shapesNoPerSecond < maxShapeInstances) {
            shapesNoPerSecond += 1;
        }
    }
    public void ShapesPerSecondDecrease()
    {
        if (shapesNoPerSecond > 0) {
            shapesNoPerSecond -= 1;
        }
    }
    public void GravityIncrease() {
        gravity += 1;
        // SetGravity();
    }
    public void GravityDecrease()
    {
        gravity -= 1;
        // SetGravity();
    }

    // private void SetGravity()
    // {
    //     if (spawnedShapes == null || spawnedShapes.Count < 1)
    //         return;

    //     foreach (var shape in spawnedShapes)
    //     {
    //         if(shape.gameObject.activeSelf)
    //             shape.SetGravity((float)(gravity / 5f));
            
    //         // // needed for a complete stop at 0 gravity
    //         // if(gravity == 0) {
    //         //     rigidBody.simulated = false;
    //         // } else {
    //         //     rigidBody.simulated = true;
    //         // }
    //     }
    // }
}
