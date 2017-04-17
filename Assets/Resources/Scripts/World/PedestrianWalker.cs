using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianWalker : MonoBehaviour {
    [SerializeField]
    private float forwardDistance;
    private Vector3 walkDirection = Vector3.forward;
    private Vector3 walkDestination;
    public NavMeshAgent navAgent;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + Vector3.up *.5f + walkDirection * forwardDistance, Vector3.down, Color.red);
    }

    void Start ()
    {
        walkDestination = transform.position + Vector3.up*.5f + walkDirection * forwardDistance;
        navAgent = GetComponent<NavMeshAgent>();
        GetComponentInChildren<Animator>().SetBool("isWalking", true);
	}
	
	// Update is called once per frame
	void Update () {
        //navAgent.destination = walkDestination;
        if (navAgent.remainingDistance <= .5f)
        {
            Ray sidewalkCheck = new Ray(transform.position + Vector3.up*.5f + walkDirection * forwardDistance, Vector3.down);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(sidewalkCheck, out hit))
            {
                if (hit.collider.tag == "Sidewalk")
                {
                    //walkDirection = Vector3.forward;
                    walkDestination = transform.position + Vector3.up * .5f + walkDirection * forwardDistance;
                    navAgent.destination = walkDestination;
                    return;
                }
                else
                {
                    int ccw = Random.Range(0, 2);
                    if (ccw == 1)
                    {
                        if (walkDirection == Vector3.forward)
                            walkDirection = Vector3.right;
                        else if (walkDirection == Vector3.right)
                            walkDirection = Vector3.back;
                        else if (walkDirection == Vector3.back)
                            walkDirection = Vector3.left;
                        else if (walkDirection == Vector3.left)
                            walkDirection = Vector3.forward;
                    }
                    else
                    {
                        if (walkDirection == Vector3.forward)
                            walkDirection = Vector3.left;
                        else if (walkDirection == Vector3.left)
                            walkDirection = Vector3.back;
                        else if (walkDirection == Vector3.back)
                            walkDirection = Vector3.right;
                        else if (walkDirection == Vector3.right)
                            walkDirection = Vector3.forward;
                    }
                    //walkDirection = -walkDirection;
                    walkDestination = transform.position + Vector3.up*.5f + walkDirection * forwardDistance;
                    //navAgent.destination = walkDestination;
                }

            }
            else // This turns the pedestrian around if they're on the edge of the world
            {
                transform.Rotate(Vector3.back);
                walkDirection = -walkDirection;
                walkDestination = transform.position + Vector3.up*.5f + walkDirection * forwardDistance;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Pedestrian>())
        {
            navAgent.destination = navAgent.destination + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2)) * .3f;
            Debug.Log("Nigga bumpin into me like dat");
        }
    }
}
