using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingFeature : MyFeature
{
    public bool isClimbing = false;

    MyCharacterMovement charMov;

    private void OnEnable()
    {
        charMov = GetComponent<MyCharacterMovement>();
    }

    protected override void Update()
    {
        //base.Update();
        WallClimbing();
    }

    void WallClimbing()
    {
        if (!isClimbing) return;
        charMov.velocityY = 0;

        float xMov = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal); //Get input response from the external InputManager "GameConstants.cs"
        float yMov = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

        Vector2 targetDir = new Vector2(xMov, yMov);
        targetDir.Normalize();


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "ClimbWall")
        {
            isClimbing = true;
        }
    }
}
