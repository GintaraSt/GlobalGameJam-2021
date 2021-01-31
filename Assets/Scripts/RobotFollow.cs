using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed = 10f;
    public float acceleration = 800f;
    public float maxSpeed = 100f;

    public ParticleSystem particles0;
    public ParticleSystem particles1;
    public ParticleSystem particles2;

    private bool huntMode = false;

    private Rigidbody rb;

    // Update is called once per frame
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (huntMode)
        {
            gameObject.transform.LookAt(playerTransform);

            rb.AddRelativeForce(Vector3.forward * acceleration * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            particles0.Play();
            particles1.Play();
            particles2.Play();
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform.GetChild(1).GetComponent<AudioSource>().Play();
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform.GetChild(0).GetComponent<AudioSource>().Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            huntMode = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            huntMode = false;
    }
}
