using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Asteroid2 : Enemy
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
                    SetSpritesVisibility(false, false);
                    StartCoroutine(AnimationRoutine());
                }else{
                    SetSpritesVisibility(false, true);
                }
                break;
            // if other is player
            case "Player":
                // damage player
                player.DamagePlayer();
                // destroy this
                SetSpritesVisibility(false, false);
                StartCoroutine(AnimationRoutine());
                break;
            default:
                break;
        }

    }

    private void SetSpritesVisibility(bool sprite1, bool sprite2){
        transform.GetChild(0).gameObject.SetActive(sprite1);
        transform.GetChild(1).gameObject.SetActive(sprite2);
    }
}
