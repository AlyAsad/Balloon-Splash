using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCollision : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 2f;
    [SerializeField] private float playerDamage = 1f;
    [SerializeField] private int maxWallBounce = 2;
    [SerializeField] private float balloonExplosionTime = 10;

    private int numOfCollisions = 0;
    private EnemyAi enemy;
    private Player player;
    [SerializeField] ParticleSystem playerBalloonSplash;
    [SerializeField] ParticleSystem enemyBalloonSplash;
    [SerializeField] ParticleSystem waterBombSplash;


    private void Start()
    {
        Destroy(gameObject, balloonExplosionTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag("Player_Balloon"))
        {
            HandlePlayerBalloonCollision(other);
        }
        else if (gameObject.CompareTag("Enemy_Balloon"))
        {
            HandleEnemyBalloonCollision(other);
        }
        else if (gameObject.CompareTag("Water_Bomb"))
        {
            HandleWaterBombCollision(other);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            if (numOfCollisions == maxWallBounce)
                Destroy(gameObject);
            else
                numOfCollisions++;
        }
    }

    private void HandleWaterBombCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyAi>();
            enemy.TakeDamage((float)1.5 * enemyDamage);
            Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplash.Play();
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplash.Play();
            Destroy(gameObject);
        }
    }
    private void HandlePlayerBalloonCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyAi>();
            enemy.TakeDamage(enemyDamage);
            Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplash.Play();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplash.Play();
            Destroy(gameObject);
        }
    }

    private void HandleEnemyBalloonCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(playerDamage);
            Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplash.Play();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player_Balloon"))
        {
            Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplash.Play();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Water_Bomb"))
        {
            Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplash.Play();
            Destroy(gameObject);
        }
    }
}


