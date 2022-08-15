using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // declare variables
    [SerializeField]
    protected float powerUpSpeed = powerupConfig.speed;

    // Update is called once per frame
    void Update()
    {
        // Move powerup down
        transform.Translate(Vector3.down * powerUpSpeed * Time.deltaTime);
        // Destroy powerup if y is greater than given height
        if(transform.position.y < config.lowerLimit){
            Destroy(this.gameObject);
        }
    }
}
