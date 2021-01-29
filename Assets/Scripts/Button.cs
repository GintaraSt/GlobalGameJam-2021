using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed = false;
    public bool pressAndHold = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        isPressed = true;    
    }

    private void OnTriggerExit(Collider other)
    {
        if(pressAndHold)
        {
            isPressed = false;
        }
    }
}
