using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float speedMultiplier = .2f;
    [SerializeField] SkinnedMeshRenderer matColor;

    IPunch punch;
    IInteract interact;
    IDrag drag;

    Rigidbody rb;
    Animator anim;

    Joystick joystick;
    CanvasManager canvas;

    bool isMoving;

    private void Awake()
    {
        interact = GetComponent<IInteract>();
        punch = GetComponent<IPunch>();
        drag = GetComponent<IDrag>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        joystick = Joystick.Instance;
        canvas = CanvasManager.Instance;
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
        interact.Interact(other);

        if (other.CompareTag("Shop"))
        {
            canvas.ShowUpgrades();

            canvas.AddCurrency(drag.Release());
            canvas.UpdateStack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interact.StopTimeInteraction();

        if (other.CompareTag("Shop"))
        {
            canvas.HideUpgrades();
        }
    }

    public void ChangeColor()
    {
        Color randomColor = new(Random.value, Random.value, Random.value);
        matColor.material.color = randomColor;
    }

    public void IncreaseStack()
    {
        drag.StackIncrease();
    }

    public int MaxStack()
    {
        return drag.MaxStack();
    }

    public int CurrentStack()
    {
        return drag.CurrentStack();
    }
}
