using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInside : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer[] rs;
    void Start()
    {
        rs = GetComponentsInChildren<Renderer>();
    }

    public void SetView(bool view) {
        foreach (Renderer r in rs)
            r.enabled = view;
    }
}
