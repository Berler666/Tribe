using UnityEngine;
using System.Collections;
using Pathfinding;

public class UnitPathFinding : MonoBehaviour {

    public Vector3 targetPosition;
    private Seeker seeker;
    private CharacterController controller;
    public Path path;

    public Transform target;

    public float speed;
    public float defaultNextWaypointDistance = 10;

    //Current waypoint
    private int currentWaypoint = 0;

    public void Start()
    {
        targetPosition = target.transform.position;
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

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //calculte direction of unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove(dir); //Unit moves here

        transform.LookAt(new Vector3(path.vectorPath[currentWaypoint].x, transform.position.y, path.vectorPath[currentWaypoint].z));

        float nextWayPointDistance = defaultNextWaypointDistance;
        if (currentWaypoint == path.vectorPath.Count - 1)
        {
            nextWayPointDistance = 0f;
        }

        //check if close enough to the current waypoint, if  yes, move on to next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }


    }
}
