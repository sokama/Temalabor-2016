using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public GameObject ExplosionParticles;
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
        GameObject explosion = (GameObject)Instantiate(ExplosionParticles, transform.position, transform.rotation);
        Destroy(explosion, 2f);

        Destroy(gameObject);
        return;
    }
}
