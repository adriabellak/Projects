using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class UploadToAPI : MonoBehaviour
{
    // The route for the api that inserts data.
    [SerializeField] string apiURL = "http://localhost:5000/sendData";
    private int levelNum;

    // We need to start a coroutine that calls the request
    public IEnumerator uploadData(int score)
    {
        // Unity sends a form, just as a html form.
        WWWForm form = new WWWForm();
        levelNum = SceneManager.GetActiveScene().buildIndex;
        // We need to create the form first, by manually adding fields. These fields match the names of the columns in the database.
        // The values from the other scripts is checked here and added to the form.
        
        form.AddField("totalMoves", score);
        form.AddField("currentLevel", levelNum);

        Debug.Log(form);

        // We create a request that makes a post to the url, and sends the form we just created.
        using (UnityWebRequest request = UnityWebRequest.Post(apiURL, form))
        {
            // The yield return line is the point at which execution will pause, and be resumed after the request ends.
            yield return request.SendWebRequest();

            // If there are no errors...
            // This line works for Unity 2019
            if (request.isNetworkError || request.isHttpError)
            // Unity 2020 will likely have to use
            //if (request.error != null)
            {
                Debug.Log(request.error);
            }
            else
            {
                // We get the response text and log it in the console.
                Debug.Log(request.downloadHandler.text);
                Debug.Log("Form upload complete!");
            }
        }
    }
}