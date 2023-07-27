using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// PlayerVFX is meant to handle the outputs for VFX and poolings
/// </para>
/// </summary>
public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private PlayerBehavior scrpt_PlyrBhvr;
    // vfx prefabs
    [SerializeField] private GameObject scn_Vfx_RunR, scn_Vfx_RunL, prfb_Vfx_Jump, prfb_Vfx_Dash;
    //vfx pools
    List<GameObject> poolVfx_Jump, poolVfx_Dash = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    // a function for checking player inputs **Ideally this would be connected the PlayerBehavior's inputs
    private void PlayerInputs()
    {
        if (Input.GetKeyDown("w"))
            print("done");//go left
        if (Input.GetKeyUp("w"))
            print("done");//stop going left

        if (Input.GetKeyDown("space"))
            print("done");//jump

        if (Input.GetKeyDown("q"))
            print("done");//dash

    }

    private void SpawnVFXPool(int _selector)
    {
        switch (_selector)
        {
            case 0: //run right
                //something
                break;
            case 1: // run left
                //something else
                break;
            case 2: // jump
                //something
                break;
            case 3: // dash

                //something
                if (scrpt_PlyrBhvr.facingRight)
                    print("90");//90
                else
                    print("-90");//-90
                break;
            default:
                //nothing
                break;
        }//end switch (_selector)

    }//end of SpawnVFXPool()


}//end of playerVFX script
