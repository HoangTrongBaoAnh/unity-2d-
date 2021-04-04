using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

    //ai's speed per second
    public float speed = 300f;
    public ForceMode2D fmode;


    [HideInInspector]
    public bool pathended = false;

    public float nextWayPointDistant = 3;

    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchPlayer());
            }
            return;
        }
        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            StartCoroutine(UpdatePath());
        }
        
    }

    IEnumerator searchPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(searchPlayer());
        }
        else
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchPlayer());
            }
            yield return false;
        }

        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            yield return new WaitForSeconds(1f / updateRate);

            StartCoroutine(UpdatePath());
        }
        

    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We get a path. Any error ?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchPlayer());
            }
            return;
        }

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathended)
            {
                return ;
            }

            Debug.Log("end of path reached");
            pathended = true;
            return;
        }

        pathended = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fmode);

        float dist = Vector3.Distance(transform.position,path.vectorPath[currentWaypoint]);

        if(dist < nextWayPointDistant)
        {
            currentWaypoint++;
            return ;
        }
    }
}
