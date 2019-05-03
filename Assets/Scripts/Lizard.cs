using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // Configurtaion Parameters
    [SerializeField] HealthBar healthBar;
    [SerializeField] float initialHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetInitialHealth(initialHealth);
        healthBar.SetBarColor(Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.HasDied())
        {
            Destroy(healthBar);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthBar.ReduceHealth(damage);
    }
}
