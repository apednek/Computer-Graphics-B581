using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLight : MonoBehaviour
{

    [SerializeField]
    Transform cameraObject, lightObject;
    [SerializeField] SpriteRenderer spriteRenderer;
    MeshRenderer meshRenderer;
    Material material;
    bool isLightOn = true;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Custom/Terrain"));
        meshRenderer.material = material;
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        if(isLightOn) material.SetInt("lightStatus", 1);
        else material.SetInt("lightStatus", 0);
        material.SetVector("lightPosition", lightObject.position);
        material.SetVector("cameraPosition", cameraObject.position);
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
