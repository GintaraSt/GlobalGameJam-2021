using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float sprintMultiplier = 1.5f;
    [SerializeField]
    private float sprintEnergyConsumption = 50;
    [SerializeField]
    private float sprintEnergyRegen = 20;
    private float sprintEnergy = 100;

    public bool dontMove = false;
    public static bool gameOver = false;

    public bool levelDone = false;

    public RectTransform energyRectTransform;

    Vector2 velocity;

    public bool hitWall = false;
    public bool isGrounded = true;

    private void Start()
    {
    }

    void FixedUpdate()
    {
        if (gameObject.scene.name == "DontDestroyOnLoad")
            DontDestroyOnLoad(gameObject);

        if (isGrounded)
            hitWall = false;

        if (!hitWall && !dontMove && !gameOver)
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift) && !(sprintEnergy <= 0))
            {
                velocity.y = velocity.y * sprintMultiplier;
                sprintEnergy -= sprintEnergyConsumption * Time.deltaTime;
            } else if (!(sprintEnergy >= 100))
            {
                sprintEnergy += sprintEnergyRegen * Time.deltaTime;
            }
            Mathf.Clamp(sprintEnergy, 0, 100);
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
            if (gameObject.GetComponent<Rigidbody>().isKinematic && levelDone)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        energyRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprintEnergy * 2.391f);   
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
