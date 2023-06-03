using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text text;
    public static int score = 0;
    
    private void Start()
    {
        SetText();
    }

    private void Update() {
        SetText();
    }
    
    public void SetText()
    {
        text.text = score.ToString();
    }
}

