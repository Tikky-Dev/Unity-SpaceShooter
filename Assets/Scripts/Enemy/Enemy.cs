using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected float speed = enemyConfig.speed;
    [SerializeField]
    protected int points;
    [SerializeField]
    protected int enemylife;
    protected Player player;
    private Animator anim;
    private Collider2D colliderEnemy;

    // audio
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosionSoundClip;


    void Start()
    {
        DefaultInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        MovemantLogic();
    }

    protected void DefaultInitialization(){
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        colliderEnemy = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        if(player == null){
            Debug.LogError("Error: Player is null");
        }
        if(anim == null){
            Debug.LogError("Error: Animator is null");
        }
        if(colliderEnemy == null){
            Debug.LogError("Error: Collider is null");
        }
        if(audioSource == null){
            Debug.LogError("Error: Audio Source is null");
        }else{
            audioSource.clip = explosionSoundClip;
        }
    }

    protected void MovemantLogic(){
        // move down
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        // respawn at the top when lower than some value with new random x position
        if(transform.position.y < config.lowerLimit){
            float randomX = Random.Range(config.leftlimit,config.rightlimit);
            transform.position = new Vector3(randomX,config.upperLimit,transform.position.z);
        }
    }


    protected void DemageEnemy(Collider2D other){
        // destroy laser
        Destroy(other.gameObject);
        enemylife--;
    }

    protected IEnumerator AnimationRoutine(){
        // trigger animation
        anim.SetTrigger("OnDeath");
        speed = 0.5f;
        colliderEnemy.enabled = !colliderEnemy.enabled;
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        // destroy this
        Destroy(this.gameObject);
    }

    
}
