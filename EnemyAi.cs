using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAi : MonoBehaviour
{

    // What to chase?
    public Transform target;//Not needed anymore

    // How many times each second we will update our path
    public float updateRate = 2f;

    // Caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

	public float activationDelay = 0.5f;
	bool active;

	Vector3 dir;

    // The rotation Speed for our enemy.
    //public float rotationSpeed = 90f;

    void Start()    
	{
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
			target = GameObject.FindWithTag ("Player").transform;
            //Debug.LogError("No Player found? PANIC!");
            //return;
        }

        // Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //TODO: Insert a player search here.
			target = GameObject.FindWithTag ("Player").transform;
        }

        // Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
		if (!active) {
			activationDelay -= Time.deltaTime;

			if (activationDelay <= 0f)
				active = true;

		} else {

			if (target == null) {
				//TODO: Insert a player search here.
				return;
			}

			//TODO: Always look at player?

			if (path == null) {
				seeker.StartPath (transform.position, target.position, OnPathComplete);
				return;
			}

			if (currentWaypoint >= path.vectorPath.Count) {
				if (pathIsEnded) {
					seeker.StartPath (transform.position, target.position, OnPathComplete);
					return;
				}

//            Debug.Log("End of path reached.");
				pathIsEnded = true;
				return;
			}
			pathIsEnded = false;

			//Direction to the next waypoint
			dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime * Mathf.Clamp((new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y).magnitude / 8f), 1f, 3f);

			//Move the AI
			rb.AddForce (dir, fMode);

			/*EnemyShoot myVisionCheck = gameObject.GetComponent<EnemyShoot> ();
		if (!myVisionCheck.enemySighted) {

			float angle = (Mathf.Atan2 (dir.y, dir.x)) * Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), rotationSpeed * Time.fixedTime);
		}*/

			float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
			if (dist < nextWaypointDistance) {
				currentWaypoint++;
				return;
			}
		}
    }

	public void rotateToPath(float rotationSpeed) {
		dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		float angle = (Mathf.Atan2 (dir.y, dir.x)) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), rotationSpeed * Time.fixedDeltaTime);
	}

}
