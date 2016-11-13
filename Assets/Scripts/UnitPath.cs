using UnityEngine;
using System.Collections;
using Pathfinding;

public class UnitPath : MonoBehaviour {

    private Seeker seeker;
    private CharacterController controller;
    public Path path;
    private Unit unit;

    public float speed;
    public float defaultNextWaypointDistance = 10;
    private int currentWaypoint = 0;

    public Transform target;
    public Vector3 targetPos;
    public Vector3 gatherPoint;

    





    public void Start()
    {

        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        unit = GetComponent<Unit>();
        targetPos = target.transform.position;
        
    }


    public void  LateUpdate()
    {
        if(unit.Selected && unit.isWalkable)
        {
            if(Input.GetMouseButtonDown(1) && Mouse.resourceCollect == false)
            {
                seeker.StartPath(transform.position, Mouse.RightClickPoint, OnPathComplete);
            }
        }

        if(unit.Selected && unit.canCollectResourses)
        {
            if(Input.GetMouseButtonDown(1) && Mouse.resourceCollect == true)
            {
                gatherPoint = Mouse.RightClickGatherPoint;
                seeker.StartPath(transform.position, gatherPoint, OnPathComplete);
            }
        }

        
        
    }

    //Path finding logic

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            //reset waypoint counter
            currentWaypoint = 0;
        }
    }


    public void FixedUpdate()
    {

        if(!unit.isWalkable)
        {
            return;
        }

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
        if(currentWaypoint == path.vectorPath.Count - 1)
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
