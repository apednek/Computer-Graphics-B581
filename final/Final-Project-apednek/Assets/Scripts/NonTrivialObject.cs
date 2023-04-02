using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class NonTrivialObject : MonoBehaviour
{
    [SerializeField]
    Transform cameraObject, lightObject;
    [SerializeField] SpriteRenderer spriteRenderer;
    Mesh mesh;
    MeshRenderer meshRenderer;
    Material material;
    Vector3[] vertices;
    int[] triangles;
    bool isLightOn = true;

    void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Custom/NonTrivial"));
        meshRenderer.material = material;
        spriteRenderer.enabled = false;
        // CreateMesh();
    }

    void Update() {
        if(isLightOn) material.SetInt("lightStatus", 1);
        else material.SetInt("lightStatus", 0);
        material.SetVector("lightPosition", lightObject.position);
        material.SetVector("cameraPosition", cameraObject.position);
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50f);

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

    public void ToggleLight() {
        if(isLightOn) {
            isLightOn = false;
            spriteRenderer.enabled = true;
        }
        else {
            isLightOn = true;
            spriteRenderer.enabled = false;
        }
    }
}
