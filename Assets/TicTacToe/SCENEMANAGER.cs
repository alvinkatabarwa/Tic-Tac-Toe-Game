using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENEMANAGER : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start button has been clicked");
        SceneManager.LoadScene("TicTacToe");
    }

    public void BacktoMainMenu()
    {
        Debug.Log("BacktoMainMenu has been clicked");
        SceneManager.LoadScene("Start Screen");
    }
}