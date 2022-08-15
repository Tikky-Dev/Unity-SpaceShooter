using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Asteroid1 : Enemy
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            // if other is laser
            case "Laser":
                DemageEnemy(other);
                if(enemylife <= 0){
                    // add points
                    player.AddScore(points);
                    StartCoroutine(AnimationRoutine());
                }
                break;
            // if other is player
            case "Player":
                // damage player
                player.DamagePlayer();
                // destroy this
                StartCoroutine(AnimationRoutine());
                break;
            default:
                break;
        }

    }
}
