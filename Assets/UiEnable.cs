using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEnable : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        UI.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        UI.SetActive(false);
    }
}
