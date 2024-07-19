using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloonCollision : MonoBehaviour
{
    //[SerializeField] EnemyAi enemy;
    [SerializeField] float damage = 2f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
            


        if (other.gameObject.CompareTag("Enemy_Balloon"))
            Destroy(gameObject);
    }
}

