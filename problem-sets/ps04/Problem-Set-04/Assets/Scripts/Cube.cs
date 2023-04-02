using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool translateXY = false;

    private bool translateXZ = false;

    private bool rotateCube = false;

    private Camera myMainCamera;
    
    private Vector2 myObjectStartPosition, myMouseStartWorldPosition, initialPosition;
    
    private Vector3 n;
    
    Vector2 dr;

    private MeshRenderer meshRenderer;

    private MeshFilter meshFilter;

    private Material material;

    private Mesh mesh;

    private const string shaderName = "CubeShader";

    Matrix4x4 modelingMatrix, rotationMatrix, translationMatrix;

    void Awake() {
        myMainCamera = Camera.main;
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        material = new Material(Shader.Find(shaderName));

        meshRenderer.material = material;
        float size = 0.5f;

        Vector3[] vertices = {
			new Vector3(-0.5f, size, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(size, size, -0.5f),
			new Vector3(size, -0.5f, -0.5f),

			new Vector3(-0.5f, -0.5f, size),
			new Vector3(size, -0.5f, size),
			new Vector3(-0.5f, size, size),
			new Vector3(size, size, size),

			new Vector3(-0.5f, size, -0.5f),
			new Vector3(size, size, -0.5f),

			new Vector3(-0.5f, size, -0.5f),
			new Vector3(-0.5f, size, size),

			new Vector3(size, size, -0.5f),
			new Vector3(size, size, size),
		};

		int[] triangles = {
			0, 2, 1, // front
			1, 2, 3,
			4, 5, 6, // back
			5, 7, 6,
			6, 7, 8, //top
			7, 9 ,8, 
			1, 3, 4, //bottom
			3, 5, 4,
			1, 11,10,// left
			1, 4, 11,
			3, 12, 5,//right
			5, 12, 13
		};
        
        mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
        // mesh.uv = uvs;
        meshFilter.mesh = mesh;
		mesh.Optimize ();
		mesh.RecalculateNormals ();
    }

    public void TranslateXY() {
        translateXY = true;
        translateXZ = false;
        rotateCube = false;
    }

    public void TranslateXZ() {
        translateXY = false;
        translateXZ = true;
        rotateCube = false;
    }

    public void RotateCube() {
        translateXY = false;
        translateXZ = false;
        rotateCube = true;
    }

    private void OnMouseDown() {

        Vector2 lMousePosition = Input.mousePosition;
        initialPosition = lMousePosition;
        myMouseStartWorldPosition = myMainCamera.ScreenToWorldPoint(lMousePosition);
        myObjectStartPosition = transform.position;

    }

    private void OnMouseDrag() {

        if(translateXY) {
            
            Debug.Log("translateXY");
            transform.Translate(new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z) * Time.deltaTime);
            translationMatrix = new Matrix4x4(
                        new Vector4(1, 0, 0, transform.position.x + 0.5f),
                        new Vector4(0, 1, 0, transform.position.y + 0.5f),
                        new Vector4(0, 0, 1, transform.position.z),
                        new Vector4(0, 0, 0, 1)
                 );

        }
        else if(translateXZ) {

            Debug.Log("translateXZ");
            transform.Translate(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f) * Time.deltaTime);
            translationMatrix = new Matrix4x4(
                        new Vector4(1, 0, 0, transform.position.x + 0.5f),
                        new Vector4(0, 1, 0, transform.position.y),
                        new Vector4(0, 0, 1, transform.position.z + 0.5f),
                        new Vector4(0, 0, 0, 1)
                 );

        }
        else if(rotateCube) {
            Debug.Log("rotating");
            // Vector2 lMousePosition = Input.mousePosition;
            // Vector2 lMouseCurrentWorldPosition = myMainCamera.ScreenToWorldPoint(lMousePosition);
            // dr = lMousePosition - initialPosition;
            // float drMagnitude = dr.magnitude;
            // n = new Vector3(-dr.y/drMagnitude, dr.x/drMagnitude, 0f);
            // float theta = drMagnitude / 50;
            // transform.Rotate(n, -theta);

            Vector2 lMousePosition = Input.mousePosition;
            Vector2 lMouseCurrentWorldPosition = myMainCamera.ScreenToWorldPoint(lMousePosition);
            dr = lMousePosition - initialPosition;
            float drMagnitude = dr.magnitude;
            float x = -dr.y/drMagnitude;
            float y = dr.x/drMagnitude;
            float z = transform.position.z;
            n = new Vector3(x, y, 0f);
            float theta = drMagnitude / 50;
            float t = 1 - Mathf.Cos(theta);
            float c = Mathf.Cos(theta);
            float s = Mathf.Sin(theta);
            transform.Rotate(n, -theta);

            rotationMatrix = new Matrix4x4(
                new Vector4(t*x*x + c, t*x*y - z*s, t*x*z + y*s, 0),
                new Vector4(t*x*y + z*s, t*y*y + c, t*y*z - x*s, 0),
                new Vector4(t*x*z - y*s, t*y*z + x*s, t*z*z + c, 0),
                new Vector4(0, 0, 0, 1)
                );
        }

        modelingMatrix = translationMatrix * rotationMatrix;
        material.SetMatrix("_ModelingMatrix", modelingMatrix);
    }
}
