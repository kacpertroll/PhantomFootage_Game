using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAnimator : MonoBehaviour
{
    public Animator zoomer;
    private bool zoomActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            zoomActive = true;
            zoomer.SetBool("zoomActive", zoomActive);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        { 
            zoomActive = false;
            zoomer.SetBool("zoomActive", zoomActive);
        }
       
    }

    
}
