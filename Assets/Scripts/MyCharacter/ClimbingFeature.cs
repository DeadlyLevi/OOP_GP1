using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingFeature : MyFeature
{
    public bool isClimbing = false;
    public float climbSmoothness = 0.2f;
    public float climbSpeed = 5f;

    MyCharacterMovement charMov;

    private void OnEnable()
    {
        charMov = GetComponent<MyCharacterMovement>();
    }

    private void OnDisable()
    {
        DeactivateClimbing();
    }

    protected override void Update()
    {
        //base.Update();
        WallClimbing();
    }

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    void WallClimbing()
    {
        if (!isClimbing) return;
        charMov.velocityY = 0;

        float xMov = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal); //Get input response from the external InputManager "GameConstants.cs"
        float yMov = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

        Vector2 targetDir = new Vector2(xMov, yMov);
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, climbSmoothness);

        charMov.velocity = (transform.up * currentDir.y + transform.right * currentDir.x) * climbSpeed;
    }

    void ActivateClimbing()
    {
        charMov.canMove = false;
    }

    void DeactivateClimbing()
    {
        charMov.canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.tag == "ClimbWall")
        {
            isClimbing = true;
            ActivateClimbing();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.gameObject.tag == "ClimbWall")
        {
            isClimbing = false;
            DeactivateClimbing();
        }
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.collider.CompareTag("ClimbWall"))
    //        Debug.Log("hit");
    //}
}
