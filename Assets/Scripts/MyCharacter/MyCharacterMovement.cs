using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterMovement : MonoBehaviour
{
    //private
    public CharacterController cc;
    float fallSpeed;

    //public 
    public float speed = 6f;
    public float JumpForce;
    public float gravity = 9.8f;
    public int maxJumps = 2;

    bool _canMove = true;
    public bool canMove { get { return _canMove; } set { _canMove = value; } }
    bool _canJump = true;
    public bool canJump { get { return _canJump; } set { _canJump = value; } }

    bool _jumpPass = false;
    public bool jumpPass { get { return _jumpPass; } set { _jumpPass = value; } }

    bool _gravityEnabled = true;
    public bool gravityEnabled { get { return _gravityEnabled; } set { _gravityEnabled = value; } }


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
        if(gravityEnabled)
            Gravity();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal);
        float vertical = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

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
                fallSpeed = JumpForce;
            }
            
        }
        
    }

    private void Gravity()
    {
        fallSpeed += -gravity * Time.deltaTime;
        cc.Move(fallSpeed * Time.deltaTime * transform.up);
    }
}
