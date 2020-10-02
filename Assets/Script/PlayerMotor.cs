using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    private Rigidbody rb;
    private Vector3 movementSpeed;
    private Vector3 horizontalRotationSpeed;
    private Vector3 verticalRotationSpeed;
    private Vector3 jumpForce;
    private bool isGrounded;

    [SerializeField]
    private float fallSpeedMultiplier = 2.5f;
    public Transform groundCheck;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void Move(Vector3 speed)
    {
        movementSpeed = speed;
    }

    public void LookHorizontal(Vector3 speed)
    {
        horizontalRotationSpeed = speed;
    }

    public void LookVertical(Vector3 speed)
    {
        verticalRotationSpeed = speed;
    }

    public void Jump(Vector3 force)
    {
        jumpForce = force;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMovement();
        PerformLookHorizontal();
        PerformLookVertical();
        PerformJump();
    }

    void PerformMovement()
    {
        if (movementSpeed != Vector3.zero)
        {
            rb.MovePosition(rb.position + movementSpeed * Time.deltaTime);
            anim.SetInteger("wri", 1);
        }
        else
        {
            anim.SetInteger("wri", 0);
        }
    }

    void PerformLookHorizontal()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(horizontalRotationSpeed));
    }

    void PerformLookVertical()
    {
        if(cam!=null)
        {
            cam.transform.Rotate(-verticalRotationSpeed);
        }
    }

    void PerformJump()
    {
        Debug.Log(isGrounded);
        if (isGrounded && jumpForce != Vector3.zero)  //Add if touching to ground
        {
            rb.AddForce(jumpForce * Time.deltaTime, ForceMode.Impulse);
            anim.SetInteger("wri", 2);

            //Debug.Log("Jumping");

           /* if(rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * gravity * fallSpeedMultiplier * Time.deltaTime; 
            }*/
        }
    }
}
