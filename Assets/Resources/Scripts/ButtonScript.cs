using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        Resources.UnloadUnusedAssets();
    } 

    public void QuitGame()
    {
        Application.Quit();
    }   

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}
