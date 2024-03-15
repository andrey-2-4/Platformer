using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoinIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypoinIndex].transform.position, transform.position) < .1f)
        {
            currentWaypoinIndex++;
            if (currentWaypoinIndex >= waypoints.Length)
            {
                currentWaypoinIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoinIndex].transform.position, Time.deltaTime * speed);
    }
}
