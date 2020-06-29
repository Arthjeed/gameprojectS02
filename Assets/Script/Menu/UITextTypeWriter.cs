using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour {

    public GameObject panel;
    Text txt;
    string story;

    void Awake () {
        txt = GetComponent<Text> ();
        story = txt.text;
        txt.text = "";
        StartCoroutine ("PlayText");

    }

    IEnumerator PlayText () {
        int i = 0;
        foreach (char c in story) {
            i++;
            txt.text += c;
            if (i == 70 || i == 190 || i == 238) {
                yield return new WaitForSeconds (2f);
                txt.text = "";
            }
            yield return new WaitForSeconds (0.050f);
        }
        yield return new WaitForSeconds (2f);
        panel.SetActive(false);
    }

}