using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV : MonoBehaviour
{
    public Transform player;
    public float rotateSpeed = 5f;
    public float lineOfSight = 0.75f; //cone size (should be greater than 0 but less than 1)

    public bool isAggro = false;


    /**void OnTriggerEnter(Collider other)
    {
        //if the player is in the enemies view set the player in sight to true
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInSight = true;
        }
    }**/
    private void Update()
    {
        Vector3 direction = player.position - transform.position;      //find direction
        float dot = Vector3.Dot(transform.forward, direction.normalized);
        if (dot > lineOfSight)
        {
            Ray ray = new Ray(transform.position, direction);                           //create ray
            RaycastHit raycastHit;

            isAggro = false;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    isAggro = true;
                    
                }
            }
        }
        else
        {
            if(direction.magnitude > 10f)
            {
                isAggro = false;
            }
            
        }
        if (isAggro)
        {
            var q = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
        }
        
    }
}
