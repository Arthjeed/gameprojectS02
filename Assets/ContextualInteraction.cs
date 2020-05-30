using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextualInteraction : MonoBehaviour
{
    [SerializeField]
    public GameObject[] ButtonList;
    [SerializeField]
    public Text[] TextList;
    public string[] StringList;
    public bool Y;
    public bool A;
    public bool B;
    public bool X;
    private bool IsVisible  = false;
    private Canvas CanvasObject;
    // Start is called before the first frame update
    void Start()
    {
        CanvasObject = GetComponent<Canvas> ();
        if (Y) {
            ButtonList[0].SetActive(true);
            //TextList[0].SetActive(true);
            TextList[0].text = StringList[0];
        }
        if (A) {
            ButtonList[1].SetActive(true);
            //TextList[1].SetActive(true);
            TextList[1].text = StringList[1];
        }
        if (B) {
            ButtonList[2].SetActive(true);
            //TextList[2].SetActive(true);
            TextList[2].text = StringList[2];
        }
        if (X) {
            ButtonList[3].SetActive(true);
            //TextList[3].SetActive(true);
            TextList[3].text = StringList[3];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility(){
        
    }
}
