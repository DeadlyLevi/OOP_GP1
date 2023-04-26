using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderFeature : MyFeature
{
    //private
    bool isGliding;
    float velocityY;

    //public 
    public float gliderSpeed = 5;
    public float rotationRate = 3;
    public float fallingSpeed = 50;

    MyCharacterMovement charMov;

    private void Start()
    {
        charMov = GetComponent<MyCharacterMovement>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DeactivateGlider();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ActivateGlider();
    }

    protected override void Update()
    {
        CanGlide();

        if(isGliding)
        {
            Rotate();
            Gravity();
            Movement();
        }

        if (charMov.cc.isGrounded)
        {
            DeactivateGlider();
            isGliding = false;
        }
    }

    void CanGlide()
    {
        if (!charMov.cc.isGrounded && Input.GetButtonDown(GameConstants.k_ButtonNameActivate))
        {
            isGliding = true;
            ActivateGlider();
        }
    }

    void Rotate()
    {
        float horizontal = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal);

        Vector3 Rotatation = new Vector3(0, rotationRate * horizontal * Time.deltaTime, 0).normalized;

        transform.Rotate(Rotatation);
    }

    void Movement()
    {

        charMov.cc.Move(transform.forward * gliderSpeed * Time.deltaTime);
    }

    void Gravity()
    {
        velocityY = -fallingSpeed * Time.deltaTime;
        charMov.cc.Move(velocityY * Time.deltaTime * transform.up);
    }

    void ActivateGlider()
    {
        if (charMov == null)
            return;
        charMov.canMove = false;
        charMov.jumpPass = false;
        charMov.gravityEnabled = false;
        charMov.orientObjectToCamera = false;
    }

    void DeactivateGlider()
    {
        if (charMov == null)
            return;

        charMov.canMove = true;
        charMov.jumpPass = true;
        charMov.gravityEnabled = true;
        charMov.orientObjectToCamera = true;
    }

}
