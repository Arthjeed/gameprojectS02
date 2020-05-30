using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public GameObject HealthBar;

    void Start()
    {
        health = maxHealth;
        HealthBar.GetComponent<Slider>().value = (health / maxHealth);
    }

    void Update()
    {
        checkDeath();
    }

    public void changeHealth(float amount)
    {
        print(amount);
        health += amount;
        HealthBar.GetComponent<Slider>().value = (health / maxHealth);
    }

    void checkDeath()
    {
        if (health <= 0)
            print("you died");
    }
}
