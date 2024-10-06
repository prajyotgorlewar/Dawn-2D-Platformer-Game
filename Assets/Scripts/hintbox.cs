using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBox : MonoBehaviour
{
    // Variables for levitation
    public float levitationSpeed = 2.0f; // Speed of the levitation
    public float levitationHeight = 0.5f; // Height of the levitation
    public GameObject hintUI; // UI to display hint

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
        
        // Make sure the hint UI is hidden at the start
        if (hintUI != null)
        {
            hintUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Levitation effect using a sine wave
        float newY = Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + newY, initialPosition.z);
    }

    // This method will be called when another object enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(true);
        }
    }

    // This method will be called when the player exits the trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(false);
        }
    }
}
