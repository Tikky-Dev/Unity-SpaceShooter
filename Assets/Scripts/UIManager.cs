using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // variables
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject RestartLevelText;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;
    [SerializeField]
    private Image lifeBlueImage;
    [SerializeField]
    private Sprite[] lifeBlueSprites;
    private int highScore = 0;
    private int score = 0;

    private bool isPlayerOneDead = false;
    private bool isPlayerTwoDead = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        highScore =  PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Best: " + highScore;
        gameOverText.SetActive(false);
        RestartLevelText.SetActive(false);
 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(gameManager == null){
            Debug.LogError("Error: Game manager is null");
        }
        if(spawnManager == null){
            Debug.LogError("Error: Spawn manager is null");
        }
    }
    public void UpdateScore(int points){
        score += points;
        scoreText.text = "Score: " + score;
    }
    public void CheckHighScore(){
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "Best: " + highScore;
        }
    }

    public void UpdateLives(int currLives, bool isPlayerOne){
        if(isPlayerOne){
            lifeImage.sprite = lifeSprites[currLives];
            
            if(currLives <= 0){
                isPlayerOneDead = true; 
            }
        }else{
            lifeBlueImage.sprite = lifeBlueSprites[currLives];

            if(currLives <= 0){ 
                isPlayerTwoDead = true;
            }
        }

        if((isPlayerOneDead && isPlayerTwoDead) || (!gameManager.getIsCoOpMode() && isPlayerOneDead)){
            doGameOver();
        }
        
    }

    private void doGameOver(){
        gameManager.GameOver();
        spawnManager.OnPlayerDeath();
        gameOverText.SetActive(true);
        RestartLevelText.SetActive(true);
        StartCoroutine(flickergameOver());
    }

    IEnumerator flickergameOver(){
        while(true){
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
