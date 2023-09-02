using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// <para> 
/// Player Behavior handles inputs and actions for the player character
/// </para>
/// </summary>
public class PlayerBehavior : MonoBehaviour
{
    [Header("HotKeys \n __________")]
    [Space]
    [Tooltip("The button we press to make Maho jump up (can be any letter or number on a normal keyboard)")]
    [SerializeField] public string buttonForJump = "space";
    [Tooltip("The button we press to make Maho dash in the direction she's moving (can be any letter or number on a normal keyboard)")]
    [SerializeField] public string buttonForDash = "w";
    [Tooltip("The button we press to move Maho backwards (can be any letter or number on a normal keyboard)")]
    [SerializeField] public string buttonForBackwards = "q";
    [Tooltip("MouseButtons can either be {0, 1, or 2}  || 0 = Left Click, 1 = Right Click, and 2 = Middle Click")]
    [SerializeField] public int buttonForShooting = 0;

    [Space]
    [Header("Platforming \n __________")]
    [Space]
    //a variable for direction
    [Tooltip("The direction maho is facing (we don't want to change this manually, we just want to see if it's happening)")]
    public bool facingRight = true;
    [Range(-1, 1)] private int directionValue = 1;

    [Tooltip("How fast Maho moves normally")]
    [SerializeField] private int speedMove = 3; //probably want to accelerate over time
    [Tooltip("How strong Maho jumps")]
    [SerializeField] private int speedJump = 500;
    [Tooltip("How powerful Maho's dash is")]
    [SerializeField] private int speedDash = 225;
    [Tooltip("The number of jumps Maho gets (before she needs to land on the ground)")]
    [SerializeField] private int jumpCountMax = 2;
    [Tooltip("The number of dashes Maho gets (before she needs to land on the ground)")]
    [SerializeField] private int dashCountMax = 3; // maybe this will be stamina/energy amount

    


    [Space]
    [Header("Shooting \n __________")]
    [Space]
    //a variable for shooting
    [Tooltip("When true, Maho should shoot out some effects")]
    public bool isShooting = true;



    [Space]
    [Header("Visuals \n __________")]
    [Space]
    //a variable for moving
    [Tooltip("When true, Maho should animate properly")]
    public bool useAnimation = true;
    [Tooltip("The Animator Component (likely found on the 3d model avatar)")]
    public Animator animController_Player;

    //private

    //-----Abilities
    [HideInInspector] public int jumpCount;
    [HideInInspector] public int dashCount;
    private int layerGround; // ground layer mask

    [SerializeField] private Rigidbody rBody;

    //-----Animation


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
        CheckAnimations();

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

    //-------------------------------------------------ANIMATIONS

    private void CheckAnimations()
    {
        //if missing refs then stop
        if (!useAnimation || !animController_Player)
            return;



    }

}//end of player behavior script
