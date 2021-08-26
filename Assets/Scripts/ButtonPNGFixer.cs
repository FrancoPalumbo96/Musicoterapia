using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPNGFixer : MonoBehaviour
{
    public void Start()
    {
        Image button = gameObject.GetComponent<Image>();
        button.alphaHitTestMinimumThreshold = 1f;
    }
}