using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public GameObject infoButton;
    public GameObject intructions;
    public GameObject toolBox;
    public GameObject toolBoxButton;
    public GameObject screen;
    public GameObject victory;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;
    
    public void LevelCompleted()
    {
        victory.SetActive(true);
        screen.SetActive(true);
        infoButton.SetActive(false);
        StartCoroutine(gameObject.GetComponent<UploadToAPI>().uploadData(score));
    }

    public void updateScore()
    {
        score ++;
        scoreText.SetText("Movimientos: {0}", score);
    }

    public void ToolBoxToggle() {
        if (toolBox.activeSelf) {
            toolBox.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            //270
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        else {
            toolBox.SetActive(true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
        }
    }

    public void InstructionsToggle()
    {
        if(intructions.activeSelf)
        {
            intructions.SetActive(false);
            screen.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            infoButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            intructions.SetActive(true);
            screen.SetActive(true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            infoButton.GetComponent<Image>().color = Color.gray;
        }
    }

    public void NextLevel()
    {
        Circuit.ClearCricuit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
