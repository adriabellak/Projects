using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject victory;
    public void LevelCompleted()
    {
        victory.SetActive(true);
    }

    public void NextLevel()
    {
        Circuit.ClearCricuit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
