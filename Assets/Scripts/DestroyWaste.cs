using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWaste : MonoBehaviour
{
    public GameObject waste;            // Reference to the waste object (optional if you want to handle specific GameObjects)
    public ParticleSystem effect;       // Particle system for the effect
    private Collider2D thisCollider;    // To store the collider of the object the script is attached to

    private void Start()
    {
        // Get the Collider2D component from the object this script is attached to
        thisCollider = GetComponent<Collider2D>();
        effect.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Disable the waste object (can be set to 'this.gameObject' if you want to disable this object)
            waste.SetActive(false);

            // Disable the collider of this object
            thisCollider.enabled = false;

            // Play the particle effect
            effect.Play();

            // Start a coroutine to destroy the object after the effect is done
            StartCoroutine(DestroyAfterEffect());
        }
    }

    private IEnumerator DestroyAfterEffect()
    {
        // Wait until the effect has finished playing
        while (effect.isPlaying)
        {
            yield return null;  // Wait for the next frame
        }

        // Destroy the waste object after the effect completes
        Destroy(gameObject);  // Destroys the object the script is attached to
    }
}
