using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private GameObject pauseMenu;

    void Start()
    {
        if(pauseMenu != null){
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver){
            StartNewGame();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P) && !isGameOver){
            if(pauseMenu != null){
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }   
        }
    }

    public void GameOver(){
        isGameOver = true;
    }

    public void StartNewGame(){
        SceneManager.LoadScene(1); // 1 is game scene
    }
    public void GoToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // 0 is menu scene
    }

    public void ResumePlay(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
