/*  CSCI-B481/B581 - Fall 2022 - Mitja Hmeljak

    This script provides "dragging" functionality.
    The mouse screen coordinates are transformed to scene (World) coordinates,
    before using the mouse position to "drag" an object in the scene.

    Original demo code by CSCI-B481 alumnus Rajin Shankar, IU Computer Science.
 */

using UnityEngine;

namespace PS01 {

    public class DragObject : MonoBehaviour {

        private Camera myMainCamera;
        private Vector2 myObjectStartPosition, myMouseStartWorldPosition;
        private float zPos, y;

        Vector3 lMousePosition, offset;
        
        private Transform _transform;
        new public Transform transform {
            get {

                // GetComponent<type>()
                //    returns the component with name type
                //        if the game object has one attached,
                //    null if it doesn't.
                return _transform ?? (_transform = GetComponent<Transform>());
                // Note: in C# the "null coalescing" operator does the following:
                //    x ?? y   Evaluates to y if x is null, to x otherwise.
            } // end of get
        } // end of public Transform transform

        void Update() {
            y = Mathf.PingPong(Time.time * 5, 8) * 6 - 3;
         transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        private void Awake() {
            myMainCamera = Camera.main;
            zPos = transform.position.z;
        }


    } // end of class DragObject

} // end of namespace PS01