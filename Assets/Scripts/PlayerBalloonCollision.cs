using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloonCollision : MonoBehaviour
{
    [SerializeField] EnemyAi enemy;
    [SerializeField] float damage = 2f;

    [SerializeField] public int maxWallBounce = 2;
    [SerializeField] public float BallonExplosionTime = 10;
    private int numOfColiision = 0;


    private void Start()
    {
        Destroy(gameObject, BallonExplosionTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //enemy.TakeDamage(damage);
            Destroy(gameObject);
        }



        if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            if (numOfColiision == maxWallBounce) Destroy(gameObject);
            else numOfColiision++;
        }
    }
}

