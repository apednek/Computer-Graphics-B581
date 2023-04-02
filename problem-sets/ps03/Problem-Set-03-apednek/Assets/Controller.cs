using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS03 {

    public class Controller : MonoBehaviour
    {
        [SerializeField] private GameObject[] gameObjects;
        private TaskC2[] task = new TaskC2[15];

        int controlPointCount = 0, currentSplineGameObjectCount = 0;

        GameObject currentSplineObject;

        public void AddControlPoint() {

            if(currentSplineGameObjectCount != 14) {

            if(currentSplineGameObjectCount == 0) {
                currentSplineObject = gameObjects[currentSplineGameObjectCount].gameObject;
            }
            
                GameObject child = currentSplineObject.transform.GetChild(controlPointCount).gameObject;
                child.SetActive(true);
                child.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, 0f);
                TaskC2 obj = currentSplineObject.GetComponent<TaskC2>();
                obj.DrawPolyLine(child);
            if(currentSplineGameObjectCount == 0 && controlPointCount == 3) {

            }
            if(controlPointCount >= 3 && (controlPointCount % 2) != 0) {
                currentSplineGameObjectCount++;
                currentSplineObject = gameObjects[currentSplineGameObjectCount].gameObject;
                controlPointCount = 0;
            }
            else {
                controlPointCount++;
            }
            }
            else {
                Debug.Log("32 control points");
            }
        }

        public void UseBezier() {
            for(int i=0; i< 15; i++) {
                task[i] = gameObjects[i].gameObject.GetComponent<TaskC2>();
                task[i].UseBezier();
            }
        }

        public void UseCatmullRom() {
            for(int i=0; i< 15; i++) {
                task[i] = gameObjects[i].gameObject.GetComponent<TaskC2>();
                task[i].UseCatmullRom();
            }
        }

        public void UseB() {
            for(int i=0; i< 15; i++) {
                task[i] = gameObjects[i].gameObject.GetComponent<TaskC2>();
                task[i].UseB();
            }
        }
    }
}
