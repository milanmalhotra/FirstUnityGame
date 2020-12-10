using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;           //set array of waypoints/destinations for the ghost

    public POV pov;
    public Animator animator;

    int m_CurrentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(pov.isAggro == false)
        {
            animator.SetBool("isIdle", true);
            navMeshAgent.isStopped = false;
            //if the remaining distance is less than the stopping distance (if the enemy is at the destination)
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                //add one to current index but if that increment puts the index = to the length of the array, set index to 0
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                //sets new destination
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }
        else
        {
            animator.SetBool("isIdle", false);
            navMeshAgent.isStopped = true;
        }
              
    }
}
