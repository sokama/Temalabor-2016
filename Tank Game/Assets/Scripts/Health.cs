﻿using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour {

    public GameObject explosionParticles;
    public GameObject healthBar;

    public float startHealth = 100f;
    public float healthRegenerationUnit = 1f;
    public float healthRegenerationTime = 1f;

    private float currentHealth;

    void Start()
    {
        currentHealth = startHealth;
        InvokeRepeating("HealthRegeneration", healthRegenerationTime, healthRegenerationTime);
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth -= damage;

        if (this.tag == "Enemy")
            HidingFromPlayer.notifiyHealth((int)currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }

        UpdateHealthBar();
    }

    private void Die()
    {
        GameObject explosion = (GameObject)Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion, 2f);

        Destroy(gameObject);
        return;
    }

    private void HealthRegeneration()
    {
        currentHealth += healthRegenerationUnit;
        if (currentHealth > startHealth)
            currentHealth = startHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.GetComponent<HealthBarManager>().UpdateHealthBar(currentHealth, startHealth);
    }
}
