using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_TripleShot : PowerUp
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            // if other is player
            case "Player":
                // give powerUp
                Player player = other.transform.GetComponent<Player>();
                player.TripleShotActivate();
                // destroy this
                Destroy(this.gameObject);
                break;
            default:
                break;
        }

    }
}
