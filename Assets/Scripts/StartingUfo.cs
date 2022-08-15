using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingUfo : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 50f;
    private Animator anim;
    private SpawnManager spawnManager;

    // audio
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosionSoundClip;

    void Start()
    {
        anim = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();
        if(anim == null){
            Debug.LogError("Error: Animator is null");
        }
        if(spawnManager == null){
            Debug.LogError("Error: Spawn manager is null");
        }
        if(audioSource == null){
            Debug.LogError("Error: Audio Source is null");
        }else{
            audioSource.clip = explosionSoundClip;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            // if other is laser
            case "Laser":
                anim.SetTrigger("OnDeath");
                spawnManager.StartSpawn();
                Destroy(other.gameObject);
                audioSource.Play();
                Destroy(this.gameObject, 2.3f);
                break;
            default:
                break;
        }

    }
}
