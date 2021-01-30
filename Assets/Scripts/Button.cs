using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed = false;
    public bool pressAndHold = false;
    public bool enableElevator = false;
    public GameObject elevator;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        isPressed = true;    
        if(enableElevator) OpenElevator();
    }

    private void OnTriggerExit(Collider other)
    {
        if(pressAndHold)
        {
            isPressed = false;
        }
    }
    public void OpenElevator(){
        elevator.gameObject.GetComponent<ElevatorController>().startReadyToLeaveSequence = true;
    }
}
