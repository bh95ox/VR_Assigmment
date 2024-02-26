using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUnload : MonoBehaviour
{
    [SerializeField] private GameObject RailPush;
    public bool IsActive;
    public bool BoxDetected;

    // Start is called before the first frame update
    void Start()
    {
        IsActive = true;

    }

    private IEnumerator TriggerDetected()
    {
        yield return new WaitForSeconds(5f);
        BoxDetected = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log("Objct Found");
            if (other.gameObject.CompareTag("Box"))
            {
                StartCoroutine(TriggerDetected());
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other != null)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                BoxDetected = false;
            }
        }
    }

}
