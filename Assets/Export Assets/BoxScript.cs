using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("BoxDetector"))
            {
                Transform LockBox = other.transform.Find("BoxParent");
                if (LockBox != null)
                {
                    Debug.Log("Oject Found");

                    gameObject.transform.SetParent(LockBox.transform);
                }
                else
                    Debug.Log("object Not found");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("BoxDetector"))
            {
                Transform LockBox = other.transform.Find("BoxParent");
                if (LockBox != null)
                {
                    Debug.Log("Oject Found");

                    gameObject.transform.SetParent(LockBox.transform);
                }
                else
                    Debug.Log("object Not found");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("BoxDetector"))
            {
                gameObject.transform.SetParent(null);
            }
        }
    }

}
