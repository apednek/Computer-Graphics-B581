using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    private bool isActive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isActive = (isActive) ? false : true;
            HUD.SetActive(isActive);
        }
    }
}
