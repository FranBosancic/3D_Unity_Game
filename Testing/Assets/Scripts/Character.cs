using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;


    private Rigidbody rb;
    private Vector3 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
    }

    void Update()
    {
        // hvatanje inputa
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        transform.position += inputDirection * moveSpeed * Time.deltaTime;
    }
}
