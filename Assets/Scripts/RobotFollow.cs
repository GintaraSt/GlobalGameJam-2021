using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed = 10f;
    public float acceleration = 800f;
    public float maxSpeed = 100f;

    private Rigidbody rb;

    // Update is called once per frame
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        gameObject.transform.LookAt(playerTransform);

        rb.AddRelativeForce(Vector3.forward * acceleration * Time.deltaTime);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
