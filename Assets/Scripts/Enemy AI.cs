using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float health = 10f, maxHealth = 10f;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] ParticleSystem deathAnimation;
    [SerializeField] Vector3 offset;
    bool death = false;


    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();

    }

    private void Start()
    {

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void FixedUpdate()
    {
        if (death)
        {
            gameObject.transform.localScale -= new Vector3(0.003f, 0.003f, 0f);
            if (gameObject.transform.localScale.x <= 0) Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            death = true;
            StartCoroutine(Die());
        }

    }
    IEnumerator Die()
    {
        deathAnimation.gameObject.SetActive(true);
        deathAnimation.Play();
        yield return new WaitForSeconds(1f);
        deathAnimation.Stop();
        deathAnimation.gameObject.SetActive(false);

    }


}
