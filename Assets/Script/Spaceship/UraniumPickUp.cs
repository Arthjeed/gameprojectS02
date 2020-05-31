using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UraniumPickUp : MonoBehaviour
{
    public float maxUranium = 5;
    public GameObject UraniumBar;

    private float currentUranium = 0;

    private void Start()
    {
        UraniumBar.GetComponent<Slider>().value = currentUranium;
    }
    public void addUranium(float amount)
    {
        currentUranium += amount;
        UraniumBar.GetComponent<Slider>().value = (currentUranium / maxUranium);
    }

    void Update()
    {
        if (currentUranium >= maxUranium)
            print("Level Completed");
    }
}
