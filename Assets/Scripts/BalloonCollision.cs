using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCollision : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 2f;
    [SerializeField] private float playerDamage = 1f;
    [SerializeField] private int maxWallBounce = 10;
    [SerializeField] private float minSpeed = 0.000f;

    private int numOfCollisions = 0;
    private EnemyAi enemy;
    private Player player;
    ThrowingBalloon playerforPowerUp;
    [SerializeField] ParticleSystem playerBalloonSplash;
    [SerializeField] ParticleSystem enemyBalloonSplash;
    [SerializeField] ParticleSystem waterBombSplash;
    [SerializeField] ParticleSystem waterBombRecoil;

    public bool justTeleported = false;

    public GameObject balloonPrefab;
    public GameObject waterBombPrefab;

    private AudioSource audioSource;
    [SerializeField] AudioClip splash, bounce, powerup, bomb;
    private GameObject trail;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!gameObject.CompareTag("Water_Bomb") && !gameObject.CompareTag("PowerUpWaterBomb")) {
            trail = transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        float speed = Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (speed < minSpeed)
        {
            Destroy(gameObject);
        }
        else
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
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

            if (other.gameObject.CompareTag("PowerUpMultiply"))
            {
                Destroy(other.gameObject);

                Vector3 temp;
                temp.x = 0.5f;
                temp.y = temp.z = 0f;

                GameObject balloon1 = Instantiate(balloonPrefab, gameObject.transform.position - temp, Quaternion.identity);
                GameObject balloon2 = Instantiate(balloonPrefab, gameObject.transform.position + temp, Quaternion.identity);

                Rigidbody2D originalRB = gameObject.GetComponent<Rigidbody2D>();
                Rigidbody2D balloon1RB = balloon1.GetComponent<Rigidbody2D>();
                Rigidbody2D balloon2RB = balloon2.GetComponent<Rigidbody2D>();

                // Copy velocity and angular velocity              
                float angle1 = 0f;//22.5f;
                Quaternion rotation1 = Quaternion.Euler(0, 0, angle1);
                balloon1RB.velocity = rotation1 * originalRB.velocity;
                balloon1RB.angularVelocity = originalRB.angularVelocity;

                float angle2 = 0f;//-22.5f;
                Quaternion rotation2 = Quaternion.Euler(0, 0, angle2);
                balloon2RB.velocity = rotation2 * originalRB.velocity;
                balloon2RB.angularVelocity = originalRB.angularVelocity;

                Destroy(balloon1, 10f);
                Destroy(balloon2, 10f);
                //Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("PowerUpHealth"))
            {
                Destroy(other.gameObject);
                player = GameObject.FindWithTag("Player").GetComponent<Player>();
                player.increaseHealth();
            }

            if (other.gameObject.CompareTag("PowerUpCoin"))
            {
                PlayPowerupSound();
                Destroy(other.gameObject);
            }
        }

        if (gameObject.CompareTag("Water_Bomb"))
        {
            if (other.gameObject.CompareTag("PowerUpMultiply"))
            {
                Destroy(other.gameObject);

                GameObject balloon1 = Instantiate(waterBombPrefab, gameObject.transform.position, Quaternion.identity);
                GameObject balloon2 = Instantiate(waterBombPrefab, gameObject.transform.position, Quaternion.identity);

                Rigidbody2D originalRB = gameObject.GetComponent<Rigidbody2D>();
                Rigidbody2D balloon1RB = balloon1.GetComponent<Rigidbody2D>();
                Rigidbody2D balloon2RB = balloon2.GetComponent<Rigidbody2D>();

                // Copy velocity and angular velocity              
                float angle1 = 22.5f;
                Quaternion rotation1 = Quaternion.Euler(0, 0, angle1);
                balloon1RB.velocity = rotation1 * originalRB.velocity;
                balloon1RB.angularVelocity = originalRB.angularVelocity;

                float angle2 = -22.5f;
                Quaternion rotation2 = Quaternion.Euler(0, 0, angle2);
                balloon2RB.velocity = rotation2 * originalRB.velocity;
                balloon2RB.angularVelocity = originalRB.angularVelocity;

                Destroy(balloon1, 10f);
                Destroy(balloon2, 10f);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("PowerUpHealth"))
            {
                Destroy(other.gameObject);
                player = GameObject.FindWithTag("Player").GetComponent<Player>();
                player.increaseHealth();
            }
        }

        if (gameObject.CompareTag("Enemy_Balloon")) {
            
            if (other.gameObject.CompareTag("Water_Bomb"))
            {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            balloonDestroy();
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
            enemy.TakeDamage((float)5 * enemyDamage);

            ParticleSystem waterBombSplashInstance = Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplashInstance.Play();
            Destroy(waterBombSplashInstance, 1f);

            ParticleSystem waterBombRecoilInstance = Instantiate(waterBombRecoil, gameObject.transform.position, Quaternion.identity);
            waterBombRecoilInstance.Play();
            Destroy(waterBombRecoilInstance, 1f);

            bombDestroy();

        }
        /*else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            ParticleSystem waterBombSplashInstance = Instantiate(waterBombSplash, gameObject.transform.position, Quaternion.identity);
            waterBombSplashInstance.Play();
            Destroy(waterBombSplashInstance, 1f);

            ParticleSystem waterBombRecoilInstance = Instantiate(waterBombRecoil, gameObject.transform.position, Quaternion.identity);
            waterBombRecoilInstance.Play();
            Destroy(waterBombRecoilInstance, 1f);

            bombDestroy();
        }*/
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



            balloonDestroy();


        }
        else if (other.gameObject.CompareTag("Enemy_Balloon"))
        {
            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Player_Balloon"))
        {
            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Enemy_Wall"))
        {
            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            ParticleSystem playerBalloonSplashInstance = Instantiate(playerBalloonSplash, gameObject.transform.position, Quaternion.identity);
            playerBalloonSplashInstance.Play();
            Destroy(playerBalloonSplashInstance, 1f);

            balloonDestroy();
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

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Player_Balloon"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Water_Bomb"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Player_Wall"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            balloonDestroy();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            ParticleSystem enemyBalloonSplashInstance = Instantiate(enemyBalloonSplash, gameObject.transform.position, Quaternion.identity);
            enemyBalloonSplashInstance.Play();
            Destroy(enemyBalloonSplashInstance, 1f);

            balloonDestroy();
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


    private void balloonDestroy()
    {
        playAudio(splash);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(trail);
        Destroy(gameObject, 1f);
    }

    private void bombDestroy()
    {
        playAudio(bomb);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }


    private void playAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1f);
    }

    private void PlayPowerupSound()
    {
        if (audioSource != null && powerup != null)
        {
            audioSource.PlayOneShot(powerup);
        }
    }


}


