using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTriangle : MonoBehaviour
{
    [SerializeField] private Transform m_goTriangle;
    [SerializeField] private Material material;
    [SerializeField] private int triangleNo;
    private Mesh m_meshTriangle;
    
    [SerializeField] private Vector3 vertice1 = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 vertice2 = new Vector3(0, 1f, 0);
    [SerializeField] private Vector3 vertice3 = new Vector3(1f, 1f, 0);

    private Vector3[] vertices;
    private GameObject newTriangle;

    // void Start() {
    //     for(int i = 0; i < triangleNo; i += 1) {
    //         newTriangle = Instantiate(new GameObject(), m_goTriangle);
    //         vertice1 = new Vector3(0, 0, 0);
    //         vertice2 = new Vector3(0, 1f, 0);
    //         vertice3 = new Vector3(1f, 1f, 0);
    //         vertices = new Vector3[]{   vertice1,
    //                                     vertice2,
    //                                     vertice3
    //                                 };
    //         NewTriangle(newTriangle, vertices);
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        // newTriangle = Instantiate(new GameObject(), m_goTriangle);
        // vertice1 = new Vector3(0, 0, 0);
        // vertice2 = new Vector3(0, 1f, 0);
        // vertice3 = new Vector3(1f, 1f, 0);
        // vertices = new Vector3[]{   vertice1,
        //                             vertice2,
        //                             vertice3
        //                         };
        // NewTriangle(newTriangle, vertices);


        // newTriangle = Instantiate(new GameObject(), m_goTriangle);
        // vertice1 = new Vector3(0, 0, 0);
        // vertice2 = new Vector3(1f, 1f, 0);
        // vertice3 = new Vector3(1f, 0, 0);
        // vertices = new Vector3[]{   vertice1,
        //                             vertice2,
        //                             vertice3
        //                         };
        // NewTriangle(newTriangle, vertices);
    }

    private void NewTriangle(GameObject triangle, Vector3[] vertices)
    {
        triangle.AddComponent<MeshFilter>();
        triangle.AddComponent<MeshRenderer>();
        var mRenderer = triangle.GetComponent<MeshRenderer>();
        mRenderer.material = material;
        m_meshTriangle = triangle.GetComponent<MeshFilter>().mesh;
        m_meshTriangle.Clear();
        m_meshTriangle.vertices = vertices;
        m_meshTriangle.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 0.25f), new Vector2(0.25f, 0.25f) };
        m_meshTriangle.triangles = new int[] { 0, 1, 2 };
    }

    // Update is called once per frame
    void Update()
    {
        vertices = new Vector3[]{   vertice1,
                                    vertice2,
                                    vertice3
                                };
        NewTriangle(m_goTriangle.gameObject, vertices);
    }
}
