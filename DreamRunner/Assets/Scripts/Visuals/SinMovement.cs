using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    public float speedUpDown = 1;
    public float distanceUpDown = 1;
    private Vector3 posTrans, posParent;
    [SerializeField] private Space relativeSpace;
    [SerializeField] private bool useParentPos;
    


    private void Start()
    {
        posTrans = transform.position;
        
    }
    void Update()
    {
        if (!useParentPos)
        {
            Vector3 mov = new Vector3(posTrans.x, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, posTrans.z);
            transform.position = posTrans + mov;
        }
        else
        {
            if (transform.parent != null)
                posParent = transform.parent.position;
            else
                return;

            Vector3 mov = new Vector3(posParent.x, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, posParent.z);
            transform.position = posParent + mov;
        }
    }
}
