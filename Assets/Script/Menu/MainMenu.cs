using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void singleplayer() {
        SceneManager.LoadScene(1);
    }
    
    public void multiplayer() {
        SceneManager.LoadScene("gameStart");
    }

    public void exitGame() {
        Application.Quit();
    }
}
