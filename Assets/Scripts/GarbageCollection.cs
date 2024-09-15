using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TMPro for TextMeshProUGUI

public class GarbageCollection : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Corrected TextMeshProUGUI
    private int score = 0;
    public GameObject GreatJob;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("waste"))
        {
            score++;
            scoreText.text = ""+score;
        }
        if(collision.gameObject.CompareTag("Dustbin"))
        {
            GreatJob.SetActive(true);
        }
    }
}
