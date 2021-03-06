﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public GameObject HealthBar;
    public GameObject GameOver;

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
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        HealthBar.GetComponent<Slider>().value = (health / maxHealth);
    }

    void checkDeath()
    {
        if (health <= 0) {
            HealthBar.SetActive(false);
            GameOver.SetActive(true);
        }
    }
}
