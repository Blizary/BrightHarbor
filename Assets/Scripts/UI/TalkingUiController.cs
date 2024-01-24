using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkingUiController : MonoBehaviour
{
    public float delay;
    public string[] strings;
    public TextMeshProUGUI currentText;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void NewText(string _text)
    {
        currentText.text = "";
        StartCoroutine(ShowText(_text));
    }

    private IEnumerator ShowText(string textToDisplay)
    {
        int stringLength = textToDisplay.Length;
        int currentIndex = 0;

        currentText.text = "";

        while (currentIndex < stringLength)
        {
            currentText.text += textToDisplay[currentIndex];
            currentIndex++;

            if (currentIndex < stringLength)
            {
                yield return new WaitForSeconds(delay);
            }
            else
            {
                break;
            }
        }

    }
}
