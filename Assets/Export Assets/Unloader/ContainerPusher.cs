using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPusher : MonoBehaviour
{
    [SerializeField] private List<GameObject> Waypoints;
    [SerializeField] private GameObject BoxDetector;
    [SerializeField] private float Speed;

    public int Index = 0;
    AutoUnload CheckUnload;
    public bool BoxPushed;

    private void Start()
    {
        CheckUnload = BoxDetector.GetComponent<AutoUnload>();
    }

    private void Update()
    {
        if(CheckUnload.BoxDetected)
        {
            BoxPushed = true;
        }

        if(CheckUnload.IsActive && BoxPushed)
        {
            Vector3 Destination = Waypoints[Index].transform.position;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, Destination, Speed * Time.deltaTime);
            transform.position = newPosition;
            float distance = Vector3.Distance(gameObject.transform.position, Destination);
            Debug.Log(distance);
            if (distance <= 0.05f)
            {
                if (Index < Waypoints.Count - 1)
                {
                    Index++;
                }

                if (Index == 3)
                {
                    BoxPushed = false;
                    //Index = 0;
                }

            }
        }        

       

    }
}
