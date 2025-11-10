using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float groundedCheckDistance;
    private float bufferCheckDistance = 0.5f; // slightly above 0

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    public Transform cameraTransform; // referenca na kameru
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Vector3 inputDirection;

    public float castDistance;
    public LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        groundedCheckDistance = capsuleCollider.height / 2 + bufferCheckDistance;
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        // towards the rotation of the camera
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // no Y component so it doesnt go into height
        camForward.y = 0;
        camRight.y = 0;

        // idk
        inputDirection = (camForward * vertical + camRight * horizontal).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    void FixedUpdate()
    {
        // moving towards the camera
        rb.MovePosition(rb.position + inputDirection * moveSpeed * Time.fixedDeltaTime);
    }

    bool isGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, groundedCheckDistance);
    }

}
