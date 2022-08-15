using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver){
            StartNewGame();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    public void GameOver(){
        isGameOver = true;
    }

    public void StartNewGame(){
        SceneManager.LoadScene(1); // 1 is game scene
    }
}
