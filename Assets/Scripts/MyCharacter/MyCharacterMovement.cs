using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterMovement : MonoBehaviour
{
    //private
    public CharacterController cc;
    Vector3 FallVelocity;

    //public 
    public float speed = 6f;
    public float JumpForce;
    public float gravity = 9.8f;
    public int maxJumps = 2;
    public bool gravityEnabled = true;

    bool _canMove = true;
    public bool canMove { get { return _canMove; } set { _canMove = value; } }

    bool _canJump = true;
    public bool canJump { get { return _canJump; } set { _canJump = value; } }

    bool _jumpPass = false;
    public bool jumpPass { get { return _jumpPass; } set { _jumpPass = value; } }


    // Start is called before the first frame update
    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove)
            Move();
        if (_canJump)
            Jump();

        Gravity();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //if (direction.magnitude >= 0.1f)
        //{
        //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        //}

        cc.Move(direction * speed * Time.deltaTime);
    }

    int jumpCount;
    private void Jump()
    {
        if(cc.isGrounded || _jumpPass)
        {
            jumpCount = 0;
        }
        
        if (jumpCount < maxJumps)
        {
            if(Input.GetButtonDown("Jump"))
            {
                jumpCount++;
                FallVelocity.y = JumpForce;
            }
            
        }
        
    }

    private void Gravity()
    {
        if(gravityEnabled == true )
        {
            FallVelocity.y += -gravity * Time.deltaTime;
            cc.Move(FallVelocity * Time.deltaTime);
        }
        
    }
}
