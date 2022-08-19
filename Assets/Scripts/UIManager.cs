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
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;
    private int highScore = 0;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        highScore =  PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Best: " + highScore;
        gameOverText.SetActive(false);
        RestartLevelText.SetActive(false);
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

    public void UpdateLives(int currLives){
        lifeImage.sprite = lifeSprites[currLives];
        if(currLives <= 0){
            doGameOver();
        }
    }

    private void doGameOver(){
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
