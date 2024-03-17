using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoinIndex = 0;

    [SerializeField] private float speed = 2f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypoinIndex].transform.position, transform.position) < .1f)
        {
            currentWaypoinIndex++;
            if (currentWaypoinIndex >= waypoints.Length)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                currentWaypoinIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoinIndex].transform.position, Time.deltaTime * speed);
    }
}
