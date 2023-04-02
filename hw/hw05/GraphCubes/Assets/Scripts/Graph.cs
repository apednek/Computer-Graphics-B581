using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
	Transform pointPrefab;

    [SerializeField, Range(10, 100)]
	int resolution = 10;

    Transform[] points;

    void Awake () {
        float step = 2f / resolution;
		var position = Vector3.zero;
		var scale = Vector3.one * step;

        points = new Transform[resolution];

        transform.localScale = new Vector3(1f, 2f, 1f);
/*
        Mesh mesh;
        Vector3[] vertices;
*/

		for (int i = 0; i < points.Length; i++) {
			Transform point = points[i] = Instantiate(pointPrefab);
            points[i] = point;
			position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
			point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);

            point.transform.Rotate(0f, Mathf.Abs(position.y * 200f), Mathf.Abs(position.y * 200f));

/*
            mesh = point.gameObject.GetComponent<MeshFilter>().mesh;
            vertices = mesh.vertices;

            Vector3[] newVertices = new Vector3[vertices.Length];
            for(int j=0; j < vertices.Length; j++) {
                Vector3 vertexWorldPosition = vertices[j];
                Vector3 vertex = vertices[j];
                if(j == 0 || j == 13 || j == 23 || j == 5 || j == 11 || j == 18) {                  
                    vertexWorldPosition.x = vertexWorldPosition.x * 2f;
                    vertexWorldPosition.y = vertexWorldPosition.y * 2f;
                    vertexWorldPosition.z = vertexWorldPosition.z * 2f;
                }
                newVertices[j] = vertexWorldPosition;
            }
            mesh.vertices = newVertices;
            
            mesh.RecalculateBounds();
*/
		}
	}

    void Start()
    {
        
    }

    
    void Update () {
        float time = Time.time;
		for (int i = 0; i < points.Length; i++) {

            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
/*
            point.transform.Rotate(0f, position.y * time, 0f);
*/            
            point.localPosition = position; 
        }
	}
}
