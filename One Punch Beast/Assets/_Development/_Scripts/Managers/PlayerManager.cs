using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;

    Rigidbody rb;
    Animator anim;
    Joystick joystick;

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
        Vector3 direction = new(horizontal, 0f, vertical);

        // Set velocity
        rb.velocity = movementSpeed * direction.normalized;

        // Set rotation
        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rot, Time.deltaTime * 1000f));
        }

        // Set animation movement
        if (rb.velocity != Vector3.zero)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
}
