using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(CapsuleCollider))]
public class NPC_WaypointNav : MonoBehaviour
{
    [SerializeField] private GameObject[] NPC_SkinModel;

    public Waypoint CurrentWaypoint;

    private NavMeshAgent NPC_Nav;
    private Animator animator;
    private int direction;
    private bool CanWalk;

    private void Awake()
    {
        NPC_Nav = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        if(NPC_SkinModel != null)
        {
            int randomSkin = Random.Range(0, NPC_SkinModel.Length);
            GameObject Model = Instantiate(NPC_SkinModel[randomSkin]);
            Model.transform.position = transform.position;
            Model.transform.forward = transform.forward;
            Model.transform.SetParent(gameObject.transform);
        }

        Animator GetAnimator = GetComponentInChildren<Animator>();
        if(GetAnimator != null) { animator = GetAnimator; }

        direction = Mathf.RoundToInt(Random.Range(0f,1f));

        NPC_Nav.enabled = false;
        NPC_Nav.enabled = true;
        NPC_Nav.SetDestination(CurrentWaypoint.GetPosition());

        CanWalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanWalk)
        {
            NPC_Movement();
        }
        

        if (NPC_Nav.isStopped)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
        
    }

    private void NPC_Movement()
    {
        if (NPC_Nav.remainingDistance < 1f)
        {
            bool canBranch = false;

            if (CurrentWaypoint.branches != null && CurrentWaypoint.branches.Count > 0)
            {
                canBranch = Random.Range(0f, 1f) <= CurrentWaypoint.branchRatio ? true : false;
            }

            if (canBranch)
            {
                CurrentWaypoint = CurrentWaypoint.branches[Random.Range(0, CurrentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if (CurrentWaypoint.NextWaypoint != null)
                    {
                        CurrentWaypoint = CurrentWaypoint.NextWaypoint;
                    }
                    else
                    {
                        CurrentWaypoint = CurrentWaypoint.PreviousWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if (CurrentWaypoint.PreviousWaypoint != null)
                    {
                        CurrentWaypoint = CurrentWaypoint.PreviousWaypoint;
                    }
                    else
                    {
                        CurrentWaypoint = CurrentWaypoint.NextWaypoint;
                        direction = 0;
                    }
                }
            }

            NPC_Nav.SetDestination(CurrentWaypoint.GetPosition());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Forklift")
        {
            CanWalk = false;
            ForkLiftHit();
        }

        if(collision.gameObject.tag == "Player")
        {
            CanWalk = false;
            PlayerHit();
        }

        if((collision.gameObject.tag == "AI"))
        {
            CanWalk = false;
            AIHit();
        }


    }

    private void ForkLiftHit()
    {

    }

    private void PlayerHit()
    {

    }

    private void AIHit()
    {

    }
}
