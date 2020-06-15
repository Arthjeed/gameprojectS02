using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public GameObject UiElement;
    private bool IsActive = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey (KeyCode.Escape))
            if (IsActive) {
                UiElement.SetActive (false);
            } else {
                UiElement.SetActive (true);
            }
        IsActive = !IsActive;
    }

    public void resume () {
        UiElement.SetActive (false);
    }
    public void exit () {
        SceneManager.LoadScene ("MainMenu");
    }
}