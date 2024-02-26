using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWorker : MonoBehaviour
{
    [SerializeField] private GameObject[] NPC_skin;


    private void Start()
    {
        int NPC_ID;
        NPC_ID = Random.Range(0, NPC_skin.Length);
        GameObject NPC_body = Instantiate(NPC_skin[NPC_ID]);

    }
}
