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
    [Header("HotKeys")]
    [Space]
    [SerializeField] public string buttonForJump = "space";
    [SerializeField] public string buttonForDash = "w";
    [SerializeField] public string buttonForBackwards = "q";

    [Space]
    [Header("Platforming")]
    [Space]
    //a variable for direction
    public bool facingRight = true;
    [Range(-1, 1)] private int directionValue = 1;

    [SerializeField] private int speedMove = 3; //probably want to accelerate over time
    [SerializeField] private int speedJump = 500;
    [SerializeField] private int speedDash = 225;
    [SerializeField] private int jumpCountMax = 2;
    [SerializeField] private int dashCountMax = 3; // maybe this will be stamina/energy amount

    [SerializeField] private Rigidbody rBody;


    //private
    [HideInInspector] public int jumpCount;
    [HideInInspector] public int dashCount;
    private int layerGround; // ground layer mask


    private void Start()
    {
        jumpCount = jumpCountMax;
        dashCount = dashCountMax;
        layerGround = LayerMask.NameToLayer("Ground");//get ground later

    }//end of start()

    private void Update()
    {
        PlayerInputs();
        AbilityMove();

    }//end of update()


    // a function for checking player inputs
    private void PlayerInputs()
    {
        if (Input.GetKeyDown(buttonForBackwards)) // backwards
            UpdateDirection(false);
        if (Input.GetKeyUp(buttonForBackwards))
            UpdateDirection(true);

        if (Input.GetKeyDown(buttonForJump)) //jump
            AbilityJump();

        if (Input.GetKeyDown(buttonForDash)) //dash
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
        if (rBody == null || jumpCount <=0) return;
        rBody.AddForce(Vector3.up * speedJump);
        jumpCount--;
    }

    // a function for dash
    private void AbilityDash()
    {
        if (dashCount <= 0) return;
        rBody.AddForce((Vector3.right * directionValue) * speedDash);
    }


    //collision with objects
    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == layerGround)
        {
            jumpCount = jumpCountMax;
            dashCount = dashCountMax;
        }
           
    }

}//end of player behavior script
