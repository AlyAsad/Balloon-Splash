using System;
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
    ThrowingBalloon playerforPowerUp;
    [SerializeField] ParticleSystem playerBalloonSplash;
    [SerializeField] ParticleSystem enemyBalloonSplash;
    [SerializeField] ParticleSystem waterBombSplash;
    [SerializeField] ParticleSystem waterBombRecoil;
    
    public bool justTeleported = false;



    private void Start()
    {
        Destroy(gameObject, balloonExplosionTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Player_Balloon"))
        {
            if (other.gameObject.CompareTag("PowerUpWaterBomb"))
            {
                playerforPowerUp = GameObject.FindWithTag("Player").GetComponent<ThrowingBalloon>();
                playerforPowerUp.setIsBomb();



                Destroy(other.gameObject);
            }
        }
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


            ParticleSystem waterBombSplashInstance = Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplashInstance.Play();
            Destroy(waterBombSplashInstance, 1f);

            ParticleSystem waterBombRecoilInstance = Instantiate(waterBombRecoil, gameObject.transform.position, Quaternion.identity);
            waterBombRecoilInstance.Play();
            Destroy(waterBombRecoilInstance, 1f);



            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            ParticleSystem waterBombSplashInstance = Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplashInstance.Play();
            Destroy(waterBombSplashInstance, 1f);



            ParticleSystem waterBombRecoilInstance = Instantiate(waterBombRecoil, gameObject.transform.position, Quaternion.identity);
            waterBombRecoilInstance.Play();
            Destroy(waterBombRecoilInstance, 1f);

            Destroy(gameObject);
        }
    }
    private void HandlePlayerBalloonCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyAi>();
            enemy.TakeDamage(enemyDamage);

            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);


            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);

            Destroy(gameObject);
        }

    }

    private void HandleEnemyBalloonCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(playerDamage);

            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player_Balloon"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Water_Bomb"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            Destroy(gameObject);
        }
    }


    public void makeTeleportedFalse(float t) 
    {
        Invoke("makeTeleportedActuallyFalse", t);
    }

    private void makeTeleportedActuallyFalse() 
    {
        justTeleported = false;
    }
}


