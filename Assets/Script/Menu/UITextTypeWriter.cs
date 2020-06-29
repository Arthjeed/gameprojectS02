using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour {

    Text txt;
    string story;

    void Awake () {
        txt = GetComponent<Text> ();
        story = txt.text;
        txt.text = "";
        StartCoroutine ("PlayText");
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    IEnumerator PlayText () {
        foreach (char c in story) {
            txt.text += c;
            yield return new WaitForSeconds (0.050f);
        }
    }

}