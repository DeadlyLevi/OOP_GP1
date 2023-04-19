using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterMovement : MonoBehaviour
{
    //private
    public Rigidbody rb;
    float gravity = 9.8f;
    float maxJumps = 2;

    //public
    [Header("Settings")]
    public float walkSpeed;
    public float walkSmoothness;
    public float jumpForce;
    public AnimationCurve jumpSmoothness;

    public bool canMove;
    public bool canJump;
    
    public Vector3 velocity;
    public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if(canMove)
            Move();
        if(canJump)
            Jump();
    }

    private void LateUpdate()
    {
        rb.velocity = velocity * walkSpeed;
    }

    // Velocity Affected by Gravity
    public float velocityY = 0.0f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    private void Move()
    {
        float xMov = Input.GetAxis(GameConstants.k_AxisNameHorizontal); //Get input response from the external InputManager "GameConstants.cs"
        float zMov = Input.GetAxis(GameConstants.k_AxisNameVertical);

        Vector3 targetDir = new Vector3(xMov, 0, zMov);

        velocity = targetDir;
    }

    int jumpCount;
    float timeInAir = 0.0f;
    public void Jump()
    {
        if (isGrounded)
        {
            jumpCount = 0;
            velocityY = 0.0f;

        }

        //Debug.Log(jumpInput);
        if (Input.GetButtonDown(GameConstants.k_ButtonNameJump))
        {
            if (jumpCount < maxJumps)
            {
                jumpCount++;
                StartCoroutine(JumpEvent(1));
            }


        }
    }

    private IEnumerator JumpEvent(float power)
    {

        timeInAir = 0.0f;
        do
        {
            float jumpMult = jumpSmoothness.Evaluate(timeInAir);
            Vector3 velocityY = new Vector3(0, jumpForce * jumpMult, 0);
            velocity += velocityY; // time * gamemanager.gameSpeed
            timeInAir += Time.fixedDeltaTime; // time * gamemanager.gameSpeed
            yield return null;
        } while (!isGrounded);
    }

    void GroundCheck()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up) * 1f, 3f, default))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;

    Vector3 FallVelocity;
    public float JumpForce;

    public float gravity = -7.5f;

    public int saltiADisposizione = 2;

    public bool gravityWall;

    public bool controllerWall;
    // Start is called before the first frame update
    void Start()
    {
        gravityWall = true;
        controllerWall = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(controllerWall == true)
        {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            //float jump = Input.GetAxisRaw("Jump");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                 if (direction.magnitude >= 0.1f)
             {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                }

            controller.Move(direction * speed * Time.deltaTime);
        }
            if(controllerWall == false)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            //float jump = Input.GetAxisRaw("Jump");
            Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }

            controller.Move(direction * speed * Time.deltaTime);
        }
        
        
        Jump();
        Gravity();
        
    }

    private void Jump()
    {
        //GroundCheck
        

        //Jump
        if (Input.GetButtonDown("Jump") && saltiADisposizione != 0)
        {
            saltiADisposizione--;
            Debug.Log("I Belive I Can Fly");
            FallVelocity.y = JumpForce;
            
        }
        
    }
    private void Gravity()
    {
        if(gravityWall == true )
        {
        FallVelocity.y += gravity * Time.deltaTime;
        controller.Move(FallVelocity * Time.deltaTime);

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            saltiADisposizione = 2;
        }

        if(other.CompareTag("Wall"))
        {
            gravityWall = false;
            controllerWall = false;
            saltiADisposizione = 2;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            gravityWall = true;
            controllerWall = true;
        }
    }
}

 */