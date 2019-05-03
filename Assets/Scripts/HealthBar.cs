using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Configurtaion Parameters
    [SerializeField] Color barColor = Color.red;

    // Variables
    GameObject hbHealth;
    GameObject hbContainer;
    GameObject hbHealthBar;
    bool hasDied = false;
    float health = 100f;


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

    public float GetHealth()
    {
        return health;
    }

    public bool HasDied()
    {
        return hasDied;
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
                //Destroy(gameObject);
                hasDied = true;
            }
        }
        else
        {
            //Destroy(gameObject);
            hasDied = true;
        }
    }

    public void SetInitialHealth(float health)
    {
        this.health = health;
    }

    public void SetBarColor(Color color)
    {
        barColor = color;
    }
}
