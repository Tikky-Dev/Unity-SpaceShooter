using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShipRam1 : Enemy
{
    private Transform target;
    private bool lockRotation = false;

    void Start()
    {
        DefaultInitialization();
        target = GameObject.Find("Player").GetComponent<Transform>();
        if(target == null){
            Debug.LogError("Error: Player(target) is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            target = GameObject.Find("HomeDestination").GetComponent<Transform>();
        }
        if(!lockRotation){
            transform.LookAt(target.position, Vector3.left);
            transform.Rotate(new Vector3(0,-90,0), Space.Self);
        }

        if(Vector3.Distance(transform.position, target.position) > 1f){
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            // if other is laser
            case "Laser":
                DemageEnemy(other);
                if(enemylife <= 0){
                    // add points
                    uiManager.UpdateScore(points);
                    transform.GetChild(0).gameObject.SetActive(false);
                    lockRotation = true;
                    StartCoroutine(AnimationRoutine());
                }
                break;
            // if other is player
            case "Player":
                player = other.GetComponent<Player>();
                // damage player
                player.DamagePlayer();
                // destroy this
                transform.GetChild(0).gameObject.SetActive(false);
                lockRotation = true;
                StartCoroutine(AnimationRoutine());
                break;
            default:
                break;
        }

    }
}
