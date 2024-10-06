using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

public class SceneChanger : MonoBehaviour
{
    public void GoToForest()
    {
        SceneManager.LoadScene("forest");
    }

    // Function to load the "Ocean" scene
    public void GoToOcean()
    {
        SceneManager.LoadScene("ocean");
    }

    // Function to load the "PollutedCityscape" scene
    public void GoToPollutedCityscape()
    {
        SceneManager.LoadScene("PollutedCity");
    }

    public void GoThome()
    {
        SceneManager.LoadScene("home");
    }
}
