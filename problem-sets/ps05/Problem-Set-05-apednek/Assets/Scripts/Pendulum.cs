using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    [SerializeField]
    private Rigidbody sphere;
    private Vector3 start, end;
    private float originalPos;
    private bool isDirectionForward = false;
    
    void Start()
    {
        start = end = transform.position;
        originalPos = transform.position.z;
    }

    void Update()
    {
        if(transform.position.z < originalPos) {
            isDirectionForward = true;
        }
        else {
            isDirectionForward = false;
        }
    }

    private void OnMouseDown() {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(!isDirectionForward)
                sphere.AddForce(transform.forward * 2000f);
            else
                sphere.AddForce(transform.forward *- 2000f);
    }
}
