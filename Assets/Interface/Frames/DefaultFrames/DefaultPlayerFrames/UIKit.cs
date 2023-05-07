using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKit : MonoBehaviour
{
    public UI_Health uiHealth;
    public UI_Clues uiClues;

    // Start is called before the first frame update
    void Start()
    {
        uiHealth = GameObject.FindObjectOfType<UI_Health>().GetComponent<UI_Health>();
        uiClues = GameObject.FindObjectOfType<UI_Clues>().GetComponent<UI_Clues>();
    }
}
