using UnityEngine;
using System.Collections;
using Pathfinding;

public class UnitPathFinding : MonoBehaviour {

    public Vector3 targetPosition;
    private Seeker seeker;
    private CharacterController controller;
    public Path path;

    public float speed;


    // the max distance from the AI to the way point for it to continue to the next waypoint
    public float nextWaypointDistance = 10;

    //Current waypoint
    private int currentWaypoint = 0;

    public void Start()
    {
        targetPosition = GameObject.Find("Target").transform.position;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();

        //Set Path
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            //reset waypoint counter
            currentWaypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if (path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //calculte direction of unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove(dir); //Unit moves here

        //check if close enough to the current waypoint, if  yes, move on to next waypoint
        if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }


    }
}
