using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    private Camera myMainCamera;
        private Vector2 myObjectStartPosition, myMouseStartWorldPosition, initialPosition;
        private Vector3 n;
        Vector2 dr;
        
        private Transform _transform;
        new public Transform transform {
            get {
                return _transform ?? (_transform = GetComponent<Transform>());
            }
        }

        private void Awake() {
            myMainCamera = Camera.main;
        }

        private void OnMouseDown() {
            Vector2 lMousePosition = Input.mousePosition;
            initialPosition = lMousePosition;
            myMouseStartWorldPosition = myMainCamera.ScreenToWorldPoint(lMousePosition);
            myObjectStartPosition = transform.position;
        }

        private void OnMouseDrag() {
            Vector2 lMousePosition = Input.mousePosition;
            Vector2 lMouseCurrentWorldPosition = myMainCamera.ScreenToWorldPoint(lMousePosition);
            dr = lMousePosition - initialPosition;
            float drMagnitude = dr.magnitude;
            n = new Vector3(-dr.y/drMagnitude, dr.x/drMagnitude, 0f);
            float theta = drMagnitude / 200;
            transform.Rotate(n, -theta);
        }
}
