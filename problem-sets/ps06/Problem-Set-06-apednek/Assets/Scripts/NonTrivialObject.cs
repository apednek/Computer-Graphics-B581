using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class NonTrivialObject : MonoBehaviour
{
    [SerializeField] private Texture texture;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        CreateMesh();
    }

    void Update() {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50f);
    }

    void CreateMesh() {
        vertices = new Vector3[]{
            new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0),
            new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0),
            new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0),
            new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0),

            new Vector3(0, 1, 0), new Vector3(0, 0, 2), new Vector3(0, 2, 0),
            new Vector3(0, 1, 0), new Vector3(2, 0, 0), new Vector3(0, 2, 0),
            new Vector3(0, 1, 0), new Vector3(-2, 0, 0), new Vector3(0, 2, 0),
            new Vector3(0, 1, 0), new Vector3(0, 0, -2), new Vector3(0, 2, 0),

            new Vector3(0, 2, 0), new Vector3(0, 0, 3), new Vector3(0, 3, 0),
            new Vector3(0, 2, 0), new Vector3(3, 0, 0), new Vector3(0, 3, 0),
            new Vector3(0, 2, 0), new Vector3(-3, 0, 0), new Vector3(0, 3, 0),
            new Vector3(0, 2, 0), new Vector3(0, 0, -3), new Vector3(0, 3, 0),
        };
        triangles = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35};

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }

}
