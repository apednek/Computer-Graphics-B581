using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAxis : MonoBehaviour
{
    public void OnDrawGizmos() {
        // draw y axis
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (transform.position + (10 * transform.up)));
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < 10; i++) {
            Gizmos.DrawLine(currentPosition + transform.up, currentPosition + transform.up + transform.right);
            Gizmos.DrawLine(currentPosition + transform.up, currentPosition + transform.up + transform.forward);
            currentPosition += Vector3.up;
        }
 
        // draw x axis
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.position + (10 * transform.right)));
        currentPosition = transform.position;
        for(int i = 0; i < 10; i++) {
            Gizmos.DrawLine(currentPosition + transform.right, currentPosition + transform.right + transform.up);
            Gizmos.DrawLine(currentPosition + transform.right, currentPosition + transform.right + transform.forward);
            currentPosition += Vector3.right;
        }
 
        // draw z axis
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (transform.position + (10 * transform.forward)));
        currentPosition = transform.position;
        for(int i = 0; i < 10; i++) {
            Gizmos.DrawLine(currentPosition + transform.forward, currentPosition + transform.forward + transform.right);
            Gizmos.DrawLine(currentPosition + transform.forward, currentPosition + transform.forward + transform.up);
            currentPosition += Vector3.forward;
        }
    }
}
