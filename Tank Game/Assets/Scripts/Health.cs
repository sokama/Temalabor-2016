using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float StartHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = StartHealth;
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        return;
    }
}
