using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float speedMultiplier = .2f;

    Rigidbody rb;
    Animator anim;
    Joystick joystick;
    Vector3 direction;

    bool isMoving;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody>();
        joystick = Joystick.Instance;
    }

    void Start()
    {
        
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
        direction = new(horizontal, 0f, vertical);

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
        if (other.CompareTag("Enemie"))
        {
            Enemie enemie = other.GetComponent<Enemie>();

            if (enemie != null)
            {
                enemie.Die(direction);
                anim.SetTrigger("punching");
            }
        }
    }
}
