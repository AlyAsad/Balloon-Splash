using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] float health = 10f, maxHealth = 10f;
    [SerializeField] EnemyHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Update()
    {
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Destroy(gameObject);
    }


}
