using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLight : MonoBehaviour
{

    [SerializeField]
    Transform cameraObject, lightObject;
    MeshRenderer mesh;
    Material material;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Custom/Terrain"));
        mesh.material = material;
    }

    void Update()
    {
        material.SetVector("lightPosition", lightObject.position);
        material.SetVector("cameraPosition", cameraObject.position);
    }
}
