using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacles script is inteded to take interactions from outside objects or player and put out a result
/// </summary>
public class Obstacles : MonoBehaviour
{
    public enum obstacleType { None, Book, Senpai, Pokki }
    public obstacleType thisObsType;

    public enum signalType { None, Dash, Bumped, Other }
    public signalType mySentSignal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Smashed(signalType _sentSignal) //consider changing to interacted
    {
        print("smashed");
    }// end of Smashed()


}//end of Obstacle Class
