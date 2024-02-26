using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SpawnBox : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private float TimeDelay = 4f;
    [SerializeField] private bool Isactive;

    float Delay;
    void Start()
    {
        Isactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Isactive)
        {

            if (CanSpawn())
            {
                Delay = 0;
                Instantiate(BoxPrefab, transform.position, transform.rotation);
                Debug.Log("spawned");
            }
            
        }
    }

    bool CanSpawn()
    {
        if(Delay < TimeDelay)
        {
            Delay += Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }
}
