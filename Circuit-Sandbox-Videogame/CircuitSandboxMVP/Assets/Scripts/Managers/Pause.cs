using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void GotToPause()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Pause");
    }

    public void LeavePause()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
