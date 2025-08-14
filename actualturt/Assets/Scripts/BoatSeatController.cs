using UnityEngine;

public class BoatSeatController : MonoBehaviour
{
    [Header("Seat Settings")]
    public Transform seatPosition;        
    public float enterDistance = 3f;      
    public KeyCode enterKey = KeyCode.F;  

    [Header("Boat Movement")]
    public float maxForwardSpeed = 12f;
    public float maxReverseSpeed = 4f;
    public float accelerationRate = 5f;   // Speed increase per second
    public float decelerationRate = 2f;   // How fast it slows when not pressing keys
    public float turnSpeed = 50f;

    private float currentSpeed = 0f;
    private bool isSeated = false;
    private Transform player;
    private CharacterController playerController; 
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 200f;
        }
        rb.linearDamping = 0.8f;       
        rb.angularDamping = 2f;
    }

    private void Update()
    {
        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null)
            {
                player = pObj.transform;
                playerController = player.GetComponent<CharacterController>();
            }
        }

        if (player != null)
        {
            if (!isSeated)
            {
                float dist = Vector3.Distance(player.position, transform.position);
                if (dist <= enterDistance && Input.GetKeyDown(enterKey))
                {
                    EnterSeat();
                }
            }
            else
            {
                if (Input.GetKeyDown(enterKey))
                {
                    ExitSeat();
                }
                else
                {
                    DriveBoat();
                }
            }
        }
    }

    private void EnterSeat()
    {
        isSeated = true;
        player.SetParent(seatPosition);
        player.position = seatPosition.position;
        player.rotation = seatPosition.rotation;

        rb.isKinematic = false;
        var moveScript = player.GetComponent<PlayerMovement>();
        if (moveScript != null)
            moveScript.enabled = false;
    }

    private void ExitSeat()
    {
        isSeated = false;
        player.SetParent(null);

        rb.linearVelocity = Vector3.zero;
        currentSpeed = 0f;
        var moveScript = player.GetComponent<PlayerMovement>();
        if (moveScript != null)
            moveScript.enabled = true;

    }

    private void DriveBoat()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Acceleration & Deceleration
        if (moveInput > 0)
        {
            currentSpeed += accelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);
        }
        else if (moveInput < 0)
        {
            currentSpeed -= accelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);
        }
        else
        {
            // Gradual slowdown when no input
            if (currentSpeed > 0)
                currentSpeed -= decelerationRate * Time.deltaTime;
            else if (currentSpeed < 0)
                currentSpeed += decelerationRate * Time.deltaTime;

            // Stop tiny floating values
            if (Mathf.Abs(currentSpeed) < 0.05f)
                currentSpeed = 0f;
        }

        // Movement
        Vector3 forwardMove = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // Turning (scales with speed)
        float turnAmount = turnInput * turnSpeed * (currentSpeed / maxForwardSpeed) * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));
    }
}
