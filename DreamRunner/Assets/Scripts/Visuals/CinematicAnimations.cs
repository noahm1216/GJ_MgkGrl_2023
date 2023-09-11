using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicAnimations : MonoBehaviour
{

    public float transformSpinTime = 0.15f;
    public float transformVFXTime = 2.0f;

    public GameObject MahoStarter, MahoPlayer, MahoSleeper;
    public Animator acMahoStart, acMahoSleep;
    public GameObject vfxTransforming;
    [Space]
    public GameObject uiStartMenu, uiEndMenu;
    [Space]
    public Camera camAnim;

    private bool gameStarted = false;

    private void Start()
    {
        //maho player invisible
        MahoPlayer.SetActive(false);
        //turn on animation cam
        camAnim.gameObject.SetActive(true);
        //maho sleeper invisible
        MahoSleeper.SetActive(false);

        //maho starter visible
        MahoStarter.SetActive(true);
    }

    public void ActivateGameFromMenu(bool _isStarting)
    {
        if (!gameStarted)
        {
            gameStarted = true;
            StartCoroutine(StartingGameAnimations(_isStarting));
        }

    }//end of StartGameFromMenu()


    private IEnumerator StartingGameAnimations(bool _startGame)
    {


        if (_startGame)
        {
            //maho starter transform animation trigger
            acMahoStart.SetTrigger("Transformed");
            //wait 0.15 seconds
            yield return new WaitForSeconds(transformSpinTime);
            //show VFX
            vfxTransforming.SetActive(true);
            //wait 2 seconds
            yield return new WaitForSeconds(transformVFXTime);

            //hide menu
            uiStartMenu.SetActive(false);
            //hide maho starter
            MahoStarter.SetActive(false);
            //turn off animation cam
            camAnim.gameObject.SetActive(false);
            //show maho player
            MahoPlayer.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            //hide vfx
            vfxTransforming.SetActive(false);
        }
        else // else finishing game
        {
            yield return new WaitForSeconds(0.15f);
            //hid maho starter
            //hide maho player

            //show maho sleeper
            //play maho sleeper animation
            //show menu buttons
        }

        yield return new WaitForSeconds(0.15f);
    }
}
