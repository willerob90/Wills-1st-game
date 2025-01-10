using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float jumpForce = 700f; // Force for jumping
    private bool isGrounded = true; // Check if the player is grounded
    
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         // Ensure the Rigidbody is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //add a forward force
        rb.AddForce(0,0,forwardForce * Time.deltaTime);

        // Move right
        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime,0,0, ForceMode.VelocityChange);
        }
        // Move left
         if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime,0,0, ForceMode.VelocityChange);
       
        }
        // Jump
        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            isGrounded = false; // Player is in the air
        }

        if (rb.position.y < -1f)
        {
            
            FindObjectOfType<GameManager>().EndGame();

        }




    }
    void OnCollisionStay(Collision collision)
    {
        // Use OnCollisionStay for more reliable ground detection
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Reset isGrounded when leaving the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
