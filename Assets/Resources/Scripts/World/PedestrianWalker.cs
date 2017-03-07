using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianWalker : MonoBehaviour {
    public float forwardDistance;
    public Vector3 walkDirection = Vector3.forward;
    private Vector3 walkDestination;
    private NavMeshAgent navAgent;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + Vector3.up + walkDirection * forwardDistance, Vector3.down, Color.red);
    }

    void Start ()
    {
        walkDestination = transform.position + Vector3.up + walkDirection * forwardDistance;
        navAgent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
        //navAgent.destination = walkDestination;
        if (!navAgent.hasPath)
        {
            Ray sidewalkCheck = new Ray(transform.position + Vector3.up + walkDirection * forwardDistance, Vector3.down);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(sidewalkCheck, out hit))
            {
                //Debug.DrawRay(transform.position + walkDirection * forwardDistance, Vector3.down, Color.red);
                Debug.Log("Collider tag: " + hit.collider.tag);
                //Debug.Break();
                if (hit.collider.tag == "Sidewalk")
                {
                    //walkDirection = Vector3.forward;
                    walkDestination = transform.position + Vector3.up + walkDirection * forwardDistance;
                    navAgent.destination = walkDestination;
                    return;
                }
                else
                {
                    walkDirection = -walkDirection;
                    walkDestination = transform.position + Vector3.up + walkDirection * forwardDistance;
                    navAgent.destination = walkDestination;
                }

            }
            else
            {
                transform.Rotate(Vector3.back);
                walkDirection = -walkDirection;
                walkDestination = transform.position + Vector3.up + walkDirection * forwardDistance;
            }
        }
	}
}
