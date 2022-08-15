using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // declare variables
    [SerializeField]
    private float laserSpeed = laseConfig.speed;

    // Update is called once per frame
    void Update()
    {
        // Move laser up
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        // Destroy laser if y is greater than given height
        if(transform.position.y >= laseConfig.distanceLimit){
            if(transform.parent != null){
                Destroy(transform.parent.gameObject);
            }      
            Destroy(this.gameObject);
        }
    }
}
