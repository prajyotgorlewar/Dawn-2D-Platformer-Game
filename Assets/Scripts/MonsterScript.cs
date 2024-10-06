using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public float speed = 2.0f;             // Speed of the monster
    public float moveDistance = 5.0f;      // Distance to move before turning around
 // Particle system for destruction effect

    private bool movingRight = true;       // Tracks the direction of movement
    private Vector2 startPosition;         // Starting position of the monster

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the monster
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
    }

    void MoveMonster()
    {
        // Calculate the current distance from the start position
        float distance = Vector2.Distance(startPosition, transform.position);

        // If the monster has reached the max distance, change direction
        if (distance >= moveDistance)
        {
            movingRight = !movingRight; // Flip the direction
            FlipSprite();               // Rotate the monster 180 degrees
            startPosition = transform.position; // Reset start position to current
        }

        // Move the monster in the current direction
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void FlipSprite()
    {
        // Rotate the sprite 180 degrees on the Y axis
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1; // Flip the X scale to rotate 180 degrees
        transform.localScale = currentScale;
    }

    // Detect collisions


}
