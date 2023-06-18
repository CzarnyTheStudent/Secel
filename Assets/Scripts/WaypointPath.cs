using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public enum PathMode { Mode2D, Mode3D };    // mode 2D or 3D
    public PathMode pathMode;                  // current mode path

    //public Color lineColor = Color.yellow;
    public Color waypointColor = Color.white;
    public Color lineBetweenWaypointsColor = Color.red;
    public float radius = 0.7f;
    public bool isClosed;
    public Transform[] waypoints;

    [Header("Move on Path")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int currentWaypointIndex;
    public bool loop;




    private void OnValidate()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].name = "Waypoint " + (i);
        }
    }

    private void Start()
    {
        //if (waypoints != null && waypoints.Length > 1)
        if (waypoints is not { Length: > 1 }) return;
        currentWaypointIndex = 0;
        transform.position = waypoints[currentWaypointIndex].position;
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length < 1)
        {
            return;
        }

        int count = waypoints.Length;

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 current = waypoints[i].position;
                Vector3 previous = Vector3.zero;

                if (i > 0)
                {
                    previous = waypoints[i - 1].position;
                }
                else if (i == 0 && isClosed)
                {
                    previous = waypoints[count - 1].position;
                }

               
            }

            if (isClosed)
            {
                Vector3 current = waypoints[0].position;
                Vector3 last = waypoints[count - 1].position;

            
            }
        }
        
        //if (waypoints != null && waypoints.Length > 1)
        if (waypoints is not { Length: > 1 }) return;
        var position = transform.position;
        position = Vector3.MoveTowards(position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);
        transform.position = position;

            
        float distanceToNextWaypoint = Vector3.Distance(position, waypoints[currentWaypointIndex].position);


        // If object is close enough to the next waypoint, move to the next waypoint
        if (distanceToNextWaypoint < radius)
        {

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            waypoints[currentWaypointIndex] = waypoints[(currentWaypointIndex ) % waypoints.Length];  //waypoints[currentWaypointIndex];
            //nextWaypoint

            if (!isClosed && currentWaypointIndex == waypoints.Length - 1 && distanceToNextWaypoint < radius)
            {
                // Reverse the array of waypoints
                System.Array.Reverse(waypoints);

                // Reset the current waypoint index
                currentWaypointIndex = 0;

                // Set the transform position to the second waypoint
                transform.position = waypoints[currentWaypointIndex].position;
            }
        }

        // Move object towards the next waypoint
        //transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);

        // If loop is disabled and object has reached the last waypoint, stop moving
        if (!loop && currentWaypointIndex == waypoints.Length - 1 && distanceToNextWaypoint < radius)
        {
            enabled = false;
        }
    }



}
//MADE BY CZARNY_ No thanks to me :0
