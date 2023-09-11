using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerBehavior scrpt_PlyrBhvr;

    [SerializeField] private Camera cameraMain;

    [SerializeField] private float distanceTolerance = 1.0f;

    [SerializeField] private float speedTransition = 0.15f;

    private float distanceToTarget = 0;

    private Transform lastCamSent;

    [SerializeField] private Transform camMid, camLeft, camRight, camBack;

    private float touchGroundTimer;
    [SerializeField] private float touchGroundTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (!cameraMain)
            cameraMain = Camera.main;

        LerpCamera(camRight.localPosition);
        lastCamSent = camRight;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    // a function for checking player inputs **Ideally this would be connected the PlayerBehavior's inputs
    private void PlayerInputs()
    {
        if (Input.GetKeyDown(scrpt_PlyrBhvr.buttonForBackwards) && scrpt_PlyrBhvr.canMove)
        {
            LerpCamera(camLeft.localPosition);//go left
            lastCamSent = camLeft;
            print("cam go left");
        }
            
        if (Input.GetKeyUp(scrpt_PlyrBhvr.buttonForBackwards) && scrpt_PlyrBhvr.canMove)
        {
            LerpCamera(camRight.localPosition);//go right
            lastCamSent = camRight;
            print("cam go right");
        }
            

        if (!scrpt_PlyrBhvr.canMove)
        {
            LerpCamera(camMid.localPosition); // go to mid
            lastCamSent = camMid;
        }
            

        if (Time.time > touchGroundTimer + touchGroundTime && scrpt_PlyrBhvr.canMove)
        {
            LerpCamera(camBack.localPosition); // zoom back
            lastCamSent = camBack;
            print("cam go back");
        }
            

        if (scrpt_PlyrBhvr.touchingGround)
        {
            touchGroundTimer = Time.time;
            if(lastCamSent == camBack)
            {
                LerpCamera(camRight.localPosition);//go right
                lastCamSent = camRight;
                print("cam return right from fall");
            }

        }

        //---------------------------------------------LERPING

        if (distanceToTarget <= distanceTolerance)
            return;
        else
        {
            if (lastCamSent)
                LerpCamera(lastCamSent.localPosition);
        }
            


    }

    private void LerpCamera(Vector3 _targetPos)
    {
        cameraMain.transform.localPosition = Vector3.Lerp(cameraMain.transform.localPosition, _targetPos, speedTransition * Time.deltaTime);
        distanceToTarget = Vector3.Distance(_targetPos, cameraMain.transform.localPosition);
    }

}//end of PlayerCamera class
