using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public GameObject questionUI; // Assign the Question UI GameObject in the Inspector
    public GameObject fumes;      // Assign the Fumes GameObject in the Inspector
    public GameObject Collider;
    public Image fillImage;        // Assign the UI Image that will fill over 10 seconds
    public float fillTime = 10f;
    private bool isFilling = false; 
    private bool hasEntered = false;



void Start()
{
    fillImage.fillAmount = 0f;
}
    // When the player enters the trigger, show the question UI
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            questionUI.SetActive(true); // Activate the question UI
            StartCoroutine(FillImageOverTime());
            hasEntered = true;
        }
        
    }

    IEnumerator FillImageOverTime()
    {
        isFilling = true; // Prevent multiple triggers
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;
            fillImage.fillAmount = elapsedTime / fillTime; // Update fill amount
            yield return null; // Wait for the next frame
        }

        fillImage.fillAmount = 1f; // Ensure it's fully filled at the end

        // Optional: Do something when the image is fully filled, e.g., hide the question UI
        Debug.Log("Image fully filled");
        isFilling = false;
    }

    // Call this function when the correct answer button is clicked
    public void OnCorrectAnswer()
    {
        // Deactivate the fumes GameObject
        fumes.SetActive(false);
        Collider.SetActive(false);

        // Optionally, you can deactivate the question UI if you want it to disappear after answering
        questionUI.SetActive(false);
    }
}
