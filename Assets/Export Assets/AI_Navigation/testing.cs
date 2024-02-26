using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour
{
    [SerializeField] GameObject[] cylinders;
    [SerializeField] private float rotationSpeed;

    public bool IsActive;

    private void Start()
    {
        IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            foreach (GameObject obj in cylinders)
            {
                obj.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
            }
        }
      
    }
}
