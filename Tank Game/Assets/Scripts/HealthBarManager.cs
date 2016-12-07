using UnityEngine;
using System.Collections;

public class HealthBarManager : MonoBehaviour {
    
    public GameObject bar;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        bar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
}
