using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorControl : MonoBehaviour
{
    public Button button;
    public Vector3 movementAxis = new Vector3(1, 0, 0);
    public float unitsToMove = 0.5f;
    public float movementSpeed = 10f;

    public bool closeOnButtonRelease = false;

    private bool isOpen = false;
    private Vector3 startingPosition;
    private void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!closeOnButtonRelease)
        {
            if (button.isPressed && !isOpen)
            {
                if ((movementAxis.x * unitsToMove) + startingPosition.x >= gameObject.transform.position.x
                    && (movementAxis.y * unitsToMove) + startingPosition.y >= gameObject.transform.position.y
                    && (movementAxis.z * unitsToMove) + startingPosition.z >= gameObject.transform.position.z)
                {
                    gameObject.transform.Translate(movementAxis * movementSpeed * Time.deltaTime);
                }
                else
                {
                    isOpen = true;
                    button.isPressed = false;
                }
            }
            else if (button.isPressed && isOpen)
            {
                if (startingPosition.x <= gameObject.transform.position.x
                    && startingPosition.y <= gameObject.transform.position.y
                    && startingPosition.z <= gameObject.transform.position.z)
                {
                    gameObject.transform.Translate(-movementAxis * movementSpeed * Time.deltaTime);
                }
                else
                {
                    isOpen = false;
                    button.isPressed = false;
                }
            }
        }
        else
        {
            if (button.isPressed)
            {
                if ((movementAxis.x * unitsToMove) + startingPosition.x >= gameObject.transform.position.x
                    && (movementAxis.y * unitsToMove) + startingPosition.y >= gameObject.transform.position.y
                    && (movementAxis.z * unitsToMove) + startingPosition.z >= gameObject.transform.position.z)
                {
                    gameObject.transform.Translate(movementAxis * movementSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (startingPosition.x <= gameObject.transform.position.x
                    && startingPosition.y <= gameObject.transform.position.y
                    && startingPosition.z <= gameObject.transform.position.z)
                {
                    gameObject.transform.Translate(-movementAxis * movementSpeed * Time.deltaTime);
                }
            }
        }
    }
}
