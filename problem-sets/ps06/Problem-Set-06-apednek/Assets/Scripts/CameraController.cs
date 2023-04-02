using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    public void rotateTop() {
        if(cam.transform.position.y < 15f)
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 3, cam.transform.position.z);
    }

    public void rotateBottom() {
        if(cam.transform.position.y > -15f)
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 3, cam.transform.position.z);
    }

    public void rotateLeft() {
        if(cam.transform.position.x > -25f)
            cam.transform.position = new Vector3(cam.transform.position.x - 3, cam.transform.position.y, cam.transform.position.z);
    }

    public void rotateRight() {
        if(cam.transform.position.x < 25f)
            cam.transform.position = new Vector3(cam.transform.position.x + 3, cam.transform.position.y, cam.transform.position.z);
    }
}
