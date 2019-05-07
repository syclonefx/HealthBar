using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Configurtaion Parameters
    [Header("Health")]
    [SerializeField] Color healthBarStandardColor = Color.red;
    [SerializeField] Color healthBarAltColor = Color.white;
    [SerializeField] [Range(0.125f, 1f)] float flashDelay = 0.25f;

    [Header("Shield")]
    [SerializeField] bool enableShield = false;
    [SerializeField] Color shieldBarColor = Color.green;
    [SerializeField] [Range(0.1f,0.75f)][Tooltip("The higher the multiplier the more damage to health.")] float shieldMultiplier = 0.5f;

    // Variables
    GameObject hbHealth;
    GameObject hbHealthBar;
    GameObject hbShieldBar;
    GameObject hbShield;

    bool hasDied = false;
    float health = 100f;
    float initialHealth = 100f;

    bool alternateColor = false;
    bool flash = false;

    bool hasShield = true;
    float shieldHealth = 100f;
    float initialShieldHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = health;
        initialShieldHealth = shieldHealth;

        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "HB_Bar")
            {
                hbHealthBar = child.gameObject;

                foreach (Transform kid in hbHealthBar.transform)
                {
                    if (kid.tag == "HB_Health")
                    {
                        hbHealth = kid.gameObject;
                    }
                }
            }
            else if (child.tag == "HB_ShieldBar")
            {
                hbShieldBar = child.gameObject;
                foreach (Transform kid in hbShieldBar.transform)
                {
                    if (kid.tag == "HB_ShieldBar")
                    {
                        hbShield = kid.gameObject;
                    }
                }
                if (!enableShield)
                {
                    hbShield.SetActive(false);
                    shieldHealth = 0;
                    initialShieldHealth = 0;
                    hasShield = false;
                }
            }
        }

        hbHealth.GetComponent<SpriteRenderer>().color = healthBarStandardColor;
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

    private IEnumerator FlashBar()
    {
        do
        {
            yield return new WaitForSeconds(flashDelay);
            if (alternateColor)
            {
                SetBarColor(healthBarStandardColor);
                alternateColor = false;
            }
            else
            {
                SetBarColor(healthBarAltColor);
                alternateColor = true;
            }
        } while (flash);
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

    private void ReduceHealth(float amount)
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

    private void ReduceShield(float amount)
    {
        if (shieldHealth >= amount)
        {
            var newShieldHealth = shieldHealth - amount;
            float percentDecrease = (newShieldHealth - shieldHealth) / 100;

            float x = hbShieldBar.transform.localScale.x;
            float newX = percentDecrease + x;
            hbShieldBar.transform.localScale = new Vector2(newX, 1f);
            shieldHealth = newShieldHealth;

            if (shieldHealth <= Mathf.Epsilon)
            {
                hasShield = false;
            }
        }
        else
        {
            hasShield = false;
        }
    }

    private void SetBarColor(Color color)
    {
        hbHealth.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetBarColors(Color standardColor, Color altColor)
    {
        healthBarStandardColor = standardColor;
        healthBarAltColor = altColor;
        hbHealth.GetComponent<SpriteRenderer>().color = standardColor;
    }

    public void SetInitialHealth(float health)
    {
        this.health = health;
        initialHealth = health;
    }

    public void TakeDamage(float amount)
    {
        if (hasShield)
        {
            ReduceShield(amount);
            float newAmount = amount * shieldMultiplier;
            ReduceHealth(newAmount);
        }
        else
        {
            ReduceHealth(amount);
        }
    }
}
