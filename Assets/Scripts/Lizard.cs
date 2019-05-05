using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // Configurtaion Parameters
    [SerializeField] HealthBar healthBar;
    [SerializeField] float initialHealth = 100f;

    // Variables


    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetInitialHealth(initialHealth);
        healthBar.SetBarColors(Color.blue, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.HasDied())
        {
            healthBar.Destroy();
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        //healthBar.ReduceShield(damage);
        //healthBar.ReduceHealth(damage / 3);
        healthBar.TakeDamage(damage);
    }
}
