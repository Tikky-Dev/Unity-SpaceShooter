using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // variables
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject RestartLevelText;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        gameOverText.SetActive(false);
        RestartLevelText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score){
        scoreText.text = "Score: " + score;
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
