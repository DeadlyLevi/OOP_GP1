using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterMovement : MonoBehaviour
{
    //private
    CharacterController cc;
    float gravity = 9.8f;
    float maxJumps = 2;

    //public
    [Header("Settings")]
    public float walkSpeed;
    public float walkSmoothness;
    public float jumpForce;
    public AnimationCurve jumpSmoothness;
    
    
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Move();
        Jump();
    }

    private void LateUpdate()
    {
        cc.Move(velocity * Time.deltaTime);
    }

    // Velocity Affected by Gravity
    public float velocityY = 0.0f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector3 velocity;
    private void Move()
    {
        float xMov = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal); //Get input response from the external InputManager "GameConstants.cs"
        float zMov = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

        Vector2 targetDir = new Vector2(zMov, xMov);
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, walkSmoothness);

        velocityY += -gravity * Time.deltaTime;

        velocity = (transform.forward * currentDir.x + transform.right * currentDir.y) * walkSpeed + transform.up * velocityY;
    }

    int jumpCount;
    float timeInAir = 0.0f;
    public void Jump()
    {
        if (cc.isGrounded)
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
        cc.slopeLimit = 90.0f;

        timeInAir = 0.0f;
        do
        {
            float jumpMult = jumpSmoothness.Evaluate(timeInAir);
            cc.Move(Vector3.up * jumpMult * (jumpForce * power) * Time.deltaTime); // time * gamemanager.gameSpeed
            timeInAir += Time.deltaTime; // time * gamemanager.gameSpeed
            yield return null;
        } while (!cc.isGrounded);

        cc.slopeLimit = 45.0f;
    }

}
