using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    [SerializeField] private int Traffic_NPCAmount;
    [SerializeField] private GameObject NPC_Prefab;

    private void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        int count = 0;
        while(count< Traffic_NPCAmount)
        {
            GameObject obj = Instantiate(NPC_Prefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount-1));
            obj.GetComponent<NPC_WaypointNav>().CurrentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.transform.position;
            yield return new WaitForEndOfFrame();

            count++;
        }
    }

}
