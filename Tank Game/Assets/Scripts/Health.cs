using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour
{
    public GameObject explosionParticles;
    public GameObject healthBar;

    public float startHealth = 100f;
    public float healthRegenerationUnit = 1f;
    public float healthRegenerationTime = 1f;
    public float damageIntakeMultiplier = 1f;

    private float currentHealth;

    void Start()
    {
        currentHealth = startHealth;
        InvokeRepeating("HealthRegeneration", healthRegenerationTime, healthRegenerationTime);
    }

    public void DecreaseHealth(float damage)
    {
        //if damage is not negative (so it's not healing the tank), we need to apply the damageIntakeMultiplier
        if (damage > 0)
            damage *= damageIntakeMultiplier;

        currentHealth -= damage;

        if (currentHealth <= 0f)
            Die();
        else if (currentHealth > startHealth)
            currentHealth = startHealth;

        notifyEnemyHealthChange();
        UpdateHealthBar();

        Debug.Log("Health: " + currentHealth);
    }

    private void Die()
    {
        GameObject explosion = (GameObject)Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion, 2f);

        GameController.instance.GameOver(this.tag);

        Destroy(gameObject);
        return;
    }

    private void HealthRegeneration()
    {
        currentHealth += healthRegenerationUnit;
        if (currentHealth > startHealth)
            currentHealth = startHealth;

        notifyEnemyHealthChange();
        UpdateHealthBar();
    }

    private void notifyEnemyHealthChange()
    {
        if (this.tag == "Enemy")
            HidingFromPlayer.notifiyHealth((int)currentHealth);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.GetComponent<HealthBarManager>().UpdateHealthBar(currentHealth, startHealth);
    }
}
