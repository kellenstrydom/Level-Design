using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    

    
    private bool sprinting;

    // [Header("Ground Check")]
    // public float playerHeight;
    // public LayerMask whatIsGround;
    // bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("SmokeBomb")] 
    public GameObject smokeBomb;
    public float throwForce;
    private KeyCode throwKey = KeyCode.E;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;
    }
    
    private void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        sprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(throwKey))
        {
            ThrowBomb();
        }
            
        
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (sprinting)
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
        rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(smokeBomb, transform.position, Quaternion.identity);
        bomb.transform.forward = orientation.forward;
        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        Vector3 throwDir = (orientation.forward + Vector3.up).normalized;

        bombRB.AddForce(throwDir * throwForce,ForceMode.Impulse);
    }
}
