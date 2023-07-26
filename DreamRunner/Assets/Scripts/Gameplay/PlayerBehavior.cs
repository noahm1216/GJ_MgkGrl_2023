using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> 
/// Player Behavior handles inputs and actions for the player character
/// </para>
/// </summary>
public class PlayerBehavior : MonoBehaviour
{

    //a variable for direction
    [SerializeField] private bool facingRight = true;
    [Range(-1, 1)] private int directionValue = 1;

    [SerializeField] private int speedMove = 3; //probably want to accelerate over time
    [SerializeField] private int speedJump = 500;
    [SerializeField] private int speedDash = 225;

    [SerializeField] private Rigidbody rBody;

    private void Update()
    {
        PlayerInputs();
        AbilityMove();

    }//end of update()


    // a function for checking player inputs
    private void PlayerInputs()
    {
        if (Input.GetKeyDown("w"))
            UpdateDirection(false);
        if (Input.GetKeyUp("w"))
            UpdateDirection(true);

        if (Input.GetKeyDown("space"))
            AbilityJump();

        if (Input.GetKeyDown("q"))
            AbilityDash();
    }

    // a fucntion to update direction as needed
    public void UpdateDirection(bool _reverseDir)
    {
        print($"going right {_reverseDir}");
        facingRight = _reverseDir;
        if (facingRight)
            directionValue = 1;
        else
            directionValue = -1;
    }

    //a function for moving
    private void AbilityMove()
    {
        transform.Translate((Vector3.right * directionValue) * (speedMove * Time.deltaTime));
    }

    // a function for the jump
    private void AbilityJump()
    {
        if (rBody == null) return;
        rBody.AddForce(Vector3.up * speedJump);
    }

    // a function for dash
    private void AbilityDash()
    {
        rBody.AddForce((Vector3.right * directionValue) * speedDash);
    }

}//end of player behavior script
