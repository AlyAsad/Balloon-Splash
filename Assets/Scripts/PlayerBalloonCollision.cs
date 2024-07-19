using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloonCollision : MonoBehaviour
{
    private EnemyAi enemy;
    private Player player;
    [SerializeField] float enemyDamage = 2f;
    [SerializeField] float playerDamage = 1f;

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
            enemy = other.gameObject.GetComponent<EnemyAi>();
            enemy.TakeDamage(enemyDamage);       
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();   
            player.TakeDamage(playerDamage);
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

