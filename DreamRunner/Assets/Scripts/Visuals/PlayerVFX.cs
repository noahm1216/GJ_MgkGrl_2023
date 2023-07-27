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
    //vfx realtime
    [SerializeField] private GameObject scn_Vfx_RunR, scn_Vfx_RunL, scn_Vfx_Jump;
    // vfx prefabs
    [SerializeField] private GameObject prfb_Vfx_Jump, prfb_Vfx_Dash;
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
            SpawnVFXPool(2);//jump

        if (Input.GetKeyDown("q"))
            SpawnVFXPool(3);//dash

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
                GameObject clnVfx_Jump = Instantiate(prfb_Vfx_Jump);
                clnVfx_Jump.transform.position = (transform.position - new Vector3(0,0.4f,0));
                scn_Vfx_Jump.GetComponent<ParticleSystem>().Play();
                Destroy(clnVfx_Jump, 3);
                break;
            case 3: // dash
                GameObject clnVfx_Dash = Instantiate(prfb_Vfx_Dash);
                clnVfx_Dash.transform.position = transform.position;
                Destroy(clnVfx_Dash, 3);
                //something
                if (scrpt_PlyrBhvr.facingRight)
                    clnVfx_Dash.transform.Rotate(0, 90, 0);//90
                else
                    clnVfx_Dash.transform.Rotate(0, -90, 0);//-90
                break;
            default:
                //nothing
                break;
        }//end switch (_selector)

    }//end of SpawnVFXPool()


}//end of playerVFX script
