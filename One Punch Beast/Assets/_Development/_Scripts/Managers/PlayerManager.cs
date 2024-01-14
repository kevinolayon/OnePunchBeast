using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float speedMultiplier = .2f;

    IPunch punch;

    Rigidbody rb;
    Animator anim;
    Joystick joystick;

    bool isMoving;

    private void Awake()
    {
        punch = GetComponent<IPunch>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        joystick = Joystick.Instance;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Get direction
        float horizontal = joystick.Horizontal();
        float vertical = joystick.Vertical();

        // Set direction
        Vector3 direction = new(horizontal, 0f, vertical);

        // Deadzone
        if (direction.magnitude > .1f)
        {
            // Set velocity
            rb.velocity = movementSpeed * direction.normalized;


            // Set rotation
            Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rot, Time.deltaTime * 1000f));
        }

        // Set animation movement and speed
        isMoving = direction.magnitude > .1f;
        float animSpeed = speedMultiplier * 1 + 1;
        anim.speed = isMoving ? Mathf.Max(animSpeed, 1) : 1;
        anim.SetBool("walking", isMoving);
    }

    private void OnTriggerEnter(Collider other)
    {
        punch.Punching(other);
    }
}
