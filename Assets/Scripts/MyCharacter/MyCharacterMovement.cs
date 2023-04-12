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
