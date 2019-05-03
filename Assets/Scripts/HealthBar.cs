using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Configurtaion Parameters
    [SerializeField] float health = 100f;
    [SerializeField] Color barColor = Color.red;

    GameObject hbHealth;
    GameObject hbContainer;
    GameObject hbHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        hbContainer = GameObject.FindWithTag("HB_Container");
        hbHealth = GameObject.FindWithTag("HB_Health");
        hbHealthBar = GameObject.FindWithTag("HB_Bar");
        hbHealth.GetComponent<SpriteRenderer>().color = barColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float amount)
    {
        if (health >= amount)
        {
            var newHealth = health - amount;
            float percentDecrease = (newHealth - health) / 100;

            float x = hbHealthBar.transform.localScale.x;
            float newX = percentDecrease + x;
            hbHealthBar.transform.localScale = new Vector2(newX, 1f);
            health = newHealth;

            if (health <= Mathf.Epsilon)
            {
                FindObjectOfType<Lizard>().Die();
                Destroy(gameObject);
            }

        }
        else
        {
            FindObjectOfType<Lizard>().Die();
            Destroy(gameObject);
        }
    }
}
