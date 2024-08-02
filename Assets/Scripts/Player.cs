using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 10f, maxHealth = 10f;
    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] ParticleSystem deathAnimation;
    bool death = false;
    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (death)
        {
            gameObject.transform.localScale -= new Vector3(0.02f, 0.02f, 0f);
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

    public void increaseHealth()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
