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
    [Tooltip("If true, the player character will try to move forward/in a direction")]
    public bool canMove = true;
    //a variable for direction
    [Tooltip("The direction maho is facing (we don't want to change this manually, we just want to see if it's happening)")]
    public bool facingRight = true;
    [Range(-1, 1)] private int directionValue = 1;
    [Tooltip("Rigidbody component we want to add force to.")]
    [SerializeField] private Rigidbody rBody;

    [Tooltip("How fast Maho moves normally")]
    [SerializeField] private int speedMove = 3; //probably want to accelerate over time
    [Tooltip("How strong Maho jumps")]
    [SerializeField] private int speedJump = 500;
    [Tooltip("How strong Maho falls while in air")]
    [SerializeField] private int speedFall = 500;
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

    [Tooltip("The prefab gameobject we want to shoot out")]
    public GameObject shootingObj;
    [Tooltip("The speed of the object we're shooting")]
    public float speedOfObj = 1.0f;
    [Tooltip("How frequently we're shooting objects")]
    public float speedFiring = 2.0f;
    



    [Space]
    [Header("Visuals+ \n __________")]
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
    [HideInInspector] public bool isDashing;
    private float lastDashTimeStamp;
    private float dashingTime;
    private int layerGround; // ground layer mask
    private int layerObstacle; // obstacle layer mask
    private bool touchingGround;

    

    //-----Animation


    private void Start()
    {
        jumpCount = jumpCountMax;
        dashCount = dashCountMax;
        dashingTime = 1.0f;
        lastDashTimeStamp = Time.time - dashingTime;
        layerGround = LayerMask.NameToLayer("Ground");//get ground later
        layerObstacle = LayerMask.NameToLayer("Obstacle");//get obstacle later
        touchingGround = true;

    }//end of start()

    private void Update()
    {
        PlayerInputs();
        AbilityMove();
        CheckAnimations();
        CheckDashing();

        if (!touchingGround)
            AddFallGravity();

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
        if (!canMove)
            return;

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
        lastDashTimeStamp = Time.time;
        rBody.AddForce((Vector3.right * directionValue) * speedDash);
    }

    private void AddFallGravity()
    {
        if (rBody == null) return;
        rBody.AddForce((Vector3.down * speedFall * Time.deltaTime), ForceMode.Acceleration);
    }//end of AddFallGravity()


    private void CheckDashing()
    {
        if (Time.time < lastDashTimeStamp + dashingTime)
            isDashing = true;
        else
            isDashing = false;
    }//end of CheckDashing()

    //-------------------------------------------------COLLISIONS

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == layerGround) // ground
        {
            jumpCount = jumpCountMax;
            dashCount = dashCountMax;
            touchingGround = true;
        }

        if (col.gameObject.layer == layerObstacle) // obstacle
        {
            //if dash and hit obstacle
            if (isDashing && col.gameObject.GetComponent<Obstacles>() != null)
            {
                var myObs = col.gameObject.GetComponent<Obstacles>();
                myObs.Smashed(myObs.mySentSignal);
            }
                
        }

    }//end of Col-Enter()

    //collision exit with objects
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == layerGround)
            touchingGround = false;
    }

    //-------------------------------------------------ANIMATIONS

    private void CheckAnimations()
    {
        //if missing refs then stop
        if (!useAnimation || !animController_Player)
            return;



    }

}//end of player behavior script
