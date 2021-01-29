using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;

    public bool hitWall = false;
    public bool isGrounded = true;

    void FixedUpdate()
    {
        if (isGrounded)
            hitWall = false;

        if (!hitWall)
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
        }
    }

    public void UnlockPlayer()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Enemy" && !isGrounded)
            hitWall = true;
    }
}
