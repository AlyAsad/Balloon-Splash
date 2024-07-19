using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloonCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);


        if (other.gameObject.CompareTag("Enemy_Balloon"))
            Destroy(gameObject); ;
    }
}

