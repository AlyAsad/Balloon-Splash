using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 10f, maxHealth = 10f;
    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] ParticleSystem deathAnimation;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    bool death = false;
    bool magic = false;
    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (death)
        {
            if (!magic) gameObject.transform.localScale -= new Vector3(0.02f, 0.02f, 0f);
            if (gameObject.transform.localScale.x <= 0)
            {
                magic = true;
                Destroy(gameObject, 1.5f);
            }
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
        audioSource.PlayOneShot(deathSound);
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
