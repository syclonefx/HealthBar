using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Configurtaion Parameters
    [SerializeField] Color barStandardColor = Color.red;
    [SerializeField] Color barAltColor = Color.white;
    [SerializeField][Range(0.125f,1f)] float flashDelay = 0.25f;

    // Variables
    GameObject hbHealth;
    GameObject hbContainer;
    GameObject hbHealthBar;
    bool hasDied = false;
    float health = 100f;
    float initialHealth = 100f;

    bool alternateColor = false;
    bool flash = false;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        hbContainer = GameObject.FindWithTag("HB_Container");
        hbHealth = GameObject.FindWithTag("HB_Health");
        hbHealthBar = GameObject.FindWithTag("HB_Bar");
        hbHealth.GetComponent<SpriteRenderer>().color = barStandardColor;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (!flash)
        {
            if (health <= (initialHealth / 3))
            {
                flash = true;
                StartCoroutine(FlashBar());
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool Flashing()
    {
        return flash;
    }

    public float GetHealth()
    {
        return health;
    }

    public bool HasDied()
    {
        return hasDied;
    }

    private IEnumerator FlashBar()
    {
        do
        {
            yield return new WaitForSeconds(flashDelay);
            if (alternateColor)
            {
                SetBarColor(barStandardColor);
                alternateColor = false;
            }
            else
            {
                SetBarColor(barAltColor);
                alternateColor = true;
            }
        } while (flash);
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
                hasDied = true;
            }
        }
        else
        {
            hasDied = true;
        }
    }

    private void SetBarColor(Color color)
    {
        hbHealth.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetBarColors(Color standardColor, Color altColor)
    {
        barStandardColor = standardColor;
        barAltColor = altColor;
        hbHealth.GetComponent<SpriteRenderer>().color = standardColor;
    }

    public void SetInitialHealth(float health)
    {
        this.health = health;
        initialHealth = health;
    }


}
