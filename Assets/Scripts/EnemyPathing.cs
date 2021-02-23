using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] Waves waveConfig;
    List<Transform> waypoints;

    // State Variables
    int waypointsIndex = 0;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointsIndex].position;
        moveSpeed = waveConfig.GetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointsIndex <= waypoints.Count - 1)
        {
            var speedPerFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                waypoints[waypointsIndex].position,
                speedPerFrame);
            if (transform.position == waypoints[waypointsIndex].position)
            {
                waypointsIndex++;
            }

        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
