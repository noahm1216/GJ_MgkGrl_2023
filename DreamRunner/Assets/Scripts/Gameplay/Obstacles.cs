using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacles script is inteded to take interactions from outside objects or player and put out a result
/// </summary>
public class Obstacles : MonoBehaviour
{   

    public enum obstacleType { None, Book, Senpai, Pokki, LoveBox }
    [Space]
    [Header("Obstacle Tag \n __________")]
    [Space]
    public obstacleType thisObsType;

    public enum signalType { None, Dash, Bump, Jump }


    [Space]
    [Header("Love Box \n __________")]
    [Space]
    //a variable for bouncing
    [Tooltip("When something sends a 'Touch' signal to the lovebox, it will bounce that object UP by this amount")]
    public int bounceHeight = 1000;



    [Space]
    [Header("Senpai \n __________")]
    [Space]
    //a variable for bouncing
    [Tooltip("When Maho dashes into this obstacle it will play the VFX gameobject below")]
    [SerializeField] private GameObject vfxBreak;


    void Start()
    {

    }// end of Start()

    public void Interacted(GameObject _interactor, signalType _sentSignal)
    {
       switch (_sentSignal)
        {
            case signalType.Dash:
                Smashed(_interactor);
                break;
            case signalType.Bump:
                Touched(_interactor);
                break;
            default:
                print($"{_sentSignal} not accounted for yet");
                break;
        }
       

    }// end of Interacted()

    //for when the player dashes up against an obstacle
    //if it has an interaction with an obstacle it will be here
    private void Smashed(GameObject _interactor)
    {
        print($"{thisObsType} was smashed");


        switch (thisObsType)
        {
            case obstacleType.Book:
                print("Smashed() - Book");
                break;
            case obstacleType.Senpai:
                print("Smashed() - Senpai");
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                GameObject deathVfx = Instantiate(vfxBreak);
                deathVfx.transform.position = transform.position + new Vector3(0,1,0);
                deathVfx.SetActive(true);
                deathVfx.transform.SetParent(transform);
                break;
            case obstacleType.Pokki:
                print("Smashed() - Pokki");
                break;
            case obstacleType.LoveBox:
                print("Smashed() - LoveBox");
                //bounce the _interactor
                int dir = 1;
                if (_interactor.transform.position.x < transform.position.x)
                    dir = -1;
                if (_interactor.transform.GetComponent<Rigidbody>() != null && _interactor.transform.position.y > transform.position.y + (transform.localScale.y * 2))
                    _interactor.transform.GetComponent<Rigidbody>().AddForce(Vector3.right * bounceHeight * dir);
                //jump reaction
                if (_interactor.transform.GetComponent<PlayerBehavior>() != null)
                    _interactor.transform.GetComponent<PlayerBehavior>().ObstacleAnimationCall("Jumped");
                break;
            default:
                print($"{thisObsType} not accounted for yet");
                break;
        }


    }//end of Smashed()

    //for when the player simply bumps up against or touches an obstacle
    //if it has an interaction with an obstacle it will be here
    private void Touched(GameObject _interactor)
    {
        print($"{thisObsType} - was touched by - {_interactor}");

        switch (thisObsType)
        {
            case obstacleType.Book:
                print("Touched() - Book");
                break;
            case obstacleType.Senpai:
                print("Touched() - Senpai");
                break;
            case obstacleType.Pokki:
                print("Touched() - Pokki");
                break;
            case obstacleType.LoveBox:
                print("Touched() - LoveBox");
                //bounce the _interactor
                if (_interactor.transform.GetComponent<Rigidbody>() != null && _interactor.transform.position.y > transform.position.y + (transform.localScale.y * 2))
                    _interactor.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceHeight);
                //jump reaction
                if (_interactor.transform.GetComponent<PlayerBehavior>() != null)
                    _interactor.transform.GetComponent<PlayerBehavior>().ObstacleAnimationCall("Jumped");
                break;
            default:
                print($"{thisObsType} not accounted for yet");
                break;
        }
    }//end of Touched()



}//end of Obstacle Class
