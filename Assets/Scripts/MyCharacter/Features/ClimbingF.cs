using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingF : MyFeature
{
    public float climbSpeed;

    public bool isClimbing = false;

    MyCharacterMovement charMov;

    ClimbingFSO SO;
    private void Awake()
    {

    }


    private void Start()
    {
        SO = featureSO as ClimbingFSO;

        climbSpeed = SO.climbSpeed;
        charMov = GetComponent<MyCharacterMovement>();
    }

    protected override void OnEnable()
    {
        //name = "Climbing";    
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DeactivateClimbing();
    }

    protected override void Update()
    {
        //base.Update();
        WallClimbing();
    }



    void WallClimbing()
    {
        if (isClimbing)
        {
            float horizontal = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal);
            float vertical = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

            Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

            //if (direction.magnitude >= 0.1f)
            //{
            //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            //}

            charMov.cc.Move(direction * climbSpeed * Time.deltaTime);
        }

    }

    void ActivateClimbing()
    {
        if (charMov == null)
            return;
        charMov.canMove = false;
        charMov.jumpPass = true;
        charMov.gravityEnabled = false;
    }

    void DeactivateClimbing()
    {
        if (charMov == null)
            return;
        charMov.canMove = true;
        charMov.jumpPass = false;
        charMov.gravityEnabled = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("ClimbWall") && !charMov.cc.isGrounded)
        {
            isClimbing = true;
            ActivateClimbing();
        }
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("ClimbWall"))
        {
            isClimbing = false;
            DeactivateClimbing();
        }
    }
}
