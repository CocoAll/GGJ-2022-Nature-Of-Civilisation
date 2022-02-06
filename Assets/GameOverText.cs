using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{

    public TextMeshProUGUI gameOverText;

    private void OnEnable()
    {
        SetUpGameOverText();
    }

    private void SetUpGameOverText()
    {
        string text = "Well done, the planet died thanks to you...\n" +
            "It survived "+ TimerText() + " time before passing away!\n\n" + 
            "Do you want to try make it survive longer ?";

        gameOverText.text = text;
    }

    private string TimerText()
    {
        string str = GameController.GetTimer() / 60 + " minute(s) " + GameController.GetTimer() % 60 + "second(s)";
        return str;
    }
}
