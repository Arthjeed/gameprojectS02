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
        Debug.Log ("tatat");
        if (Input.GetKeyUp (KeyCode.Escape)) {
            Debug.Log ("toto " + IsActive);
            UiElement.SetActive(!IsActive);
            IsActive = !IsActive;
        }
    }

    public void resume () {
        UiElement.SetActive (false);
        IsActive = false;
    }
    public void exit () {
        SceneManager.LoadScene ("MainMenu");
    }

    
}