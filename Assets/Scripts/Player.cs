using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // declare variables
    [SerializeField]
    private float speed = playerConfig.speed;
    [SerializeField]
    private float fireRate = laseConfig.fireRate;
    private float canFire = -1f;
    [SerializeField]
    private int lives = playerConfig.lives;
    private SpawnManager spawnManager;
    // tripleShot active
    [SerializeField]
    private bool canTripleShot = false;
    [SerializeField]
    private bool canSpeedBoost = false;
    [SerializeField]
    private bool isSpeedBoost = false;
    [SerializeField]
    private bool canShield = false;
    

    [SerializeField]
    private GameObject shieldVisual1;
    [SerializeField]
    private GameObject shieldVisual2;
    [SerializeField]
    private GameObject shieldVisual3;

    private int shieldLives = 0;
    [SerializeField]
    private int score = 0;
    private UIManager  uiManager;
    private GameManager  gameManager;

    // prefabs
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShot;

    // audio
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip laserSoundClip;
    [SerializeField]
    private AudioClip TripleShotSoundClip;
    [SerializeField]
    private AudioClip ShieldUpSoundClip;
    [SerializeField]
    private AudioClip ShieldDownSoundClip;
    [SerializeField]
    private AudioClip PowerupPickupSoundClip;
    void Start()
    {
        // Set startin position to (0, 0, 0)
        transform.position = new Vector3(playerConfig.startXPosition, playerConfig.startYPosition, playerConfig.startZPosition);
        // find span manager
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        if(spawnManager == null){
            Debug.LogError("Error: Spawn manager is null");
        }
        if(uiManager == null){
            Debug.LogError("Error: Ui manager is null");
        }
        if(gameManager == null){
            Debug.LogError("Error: Game manager is null");
        }
        if(audioSource == null){
            Debug.LogError("Error: Audio Source is null");
        }else{
            audioSource.clip = laserSoundClip;
        }

        uiManager.UpdateLives(lives);

    }

    void Update()
    {
       Movemantlogic();
       
        // Using space key to fire laser object
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire){
            FireLaser();
        }
    }

    void Movemantlogic(){
        // Inputs: Horizontal and Vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // Move player to the given direction with preset speed
        transform.Translate(direction * speed * Time.deltaTime);

        // Limit player movement on Y axises
            // upper and lower limit
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, playerConfig.lowerLimit, playerConfig.upperLimit), transform.position.z);
        // Limit player movement on X axises
            // right and left limit
        if(transform.position.x >= playerConfig.rightlimit)
        {
            transform.position = new Vector3(playerConfig.leftlimit, transform.position.y, transform.position.z);
        }else if(transform.position.x <= playerConfig.leftlimit)
        {
            transform.position = new Vector3(playerConfig.rightlimit, transform.position.y, transform.position.z);
        }
    }

    void FireLaser(){
        audioSource.clip = laserSoundClip;
        canFire = Time.time + fireRate;

        // if canTripleShot = true fire triple shot
        if(canTripleShot){
            audioSource.clip = TripleShotSoundClip;
            Instantiate(tripleShot, transform.position, Quaternion.identity);
            audioSource.Play();    
            return;
        }

        Instantiate(laserPrefab, transform.position + laseConfig.offsetSpawn, Quaternion.identity);
        audioSource.Play();
    
    }

    public void DamagePlayer(){
        audioSource.clip = ShieldDownSoundClip;
        // remove life
        if(canShield){
            shieldLives--;
            switch (shieldLives)
            {
                 case 1:
                    shieldVisual2.SetActive(false);
                    shieldVisual1.SetActive(true);
                    audioSource.Play();
                    break;
                case 2:
                    shieldVisual3.SetActive(false);
                    shieldVisual2.SetActive(true);
                    audioSource.Play();
                    break;     
                default:
                    canShield = false;
                    shieldVisual1.SetActive(false);
                    audioSource.Play();
                    break;
            }
            return;
        }
        lives--;
        uiManager.UpdateLives(lives);

        // check if dead
        if(lives <= 0){

            // Stop spawning
            if(spawnManager != null){
                gameManager.GameOver();
                spawnManager.OnPlayerDeath();
            }
            uiManager.CheckHighScore();
            // destroy this
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActivate(){
        audioSource.clip = PowerupPickupSoundClip;
        canTripleShot = true;
        // start coroutine
        StartCoroutine(deactivateTripleShot());
        audioSource.Play();
    }
    public void SpeedBoostActivate(){
        audioSource.clip = PowerupPickupSoundClip;
        canSpeedBoost = true;
        if(!isSpeedBoost){
            isSpeedBoost = true;
            speed *= 2;
            StartCoroutine(deactivateSpeedBoost());
        }
        // start coroutine
        audioSource.Play();
    }
    public void ShieldActivate(){
        audioSource.clip = ShieldUpSoundClip;
        canShield = true;
        if(shieldLives < 3){
            shieldLives++;
        }

        switch (shieldLives)
        {   
            case 2:
                shieldVisual1.SetActive(false);
                shieldVisual2.SetActive(true);
                audioSource.Play();
                break;
            case 3:
                shieldVisual2.SetActive(false);
                shieldVisual3.SetActive(true);
                audioSource.Play();
                break;     
            default:
                shieldVisual1.SetActive(true);
                audioSource.Play();
                break;
        }
    }

    public void AddScore(int points){
        score += points;
        uiManager.UpdateScore(score);
    }
    // Spawn object every given time interval
    IEnumerator deactivateTripleShot(){
        while(canTripleShot){
            // wait for given time
            yield return new WaitForSeconds(powerupConfig.timeLimit);
            canTripleShot = false;
        }
    }
    // Spawn object every given time interval
    IEnumerator deactivateSpeedBoost(){
        while(canSpeedBoost){
            // wait for given time
            yield return new WaitForSeconds(powerupConfig.timeLimit);
            canSpeedBoost = false;
            isSpeedBoost = false;
            speed /= 2;
        }
    }
}
