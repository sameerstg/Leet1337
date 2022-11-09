using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberGame : MonoBehaviour
{
    public static NumberGame _instance;
    public TextMeshProUGUI questionText;
    public TMP_InputField textInput;
    int level;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        StartGameFromStart();
    }
    private void StartGameFromStart()
    {
        level = 0;
        SetNewQuestion();
    }
    void StartNextLevel()
    {

        level += 1;
        SetNewQuestion();
        print("dobe");
    }
    void SetNewQuestion()
    {
        questionText.gameObject.SetActive(true);
        textInput.text = "";
        questionText.text = "";
        for (int i = 0; i < level + 1; i++)
        {
            questionText.text += GetRandomNumber();
        }
        textInput.gameObject.SetActive(false);
        textInput.gameObject.SetActive(true);


    }
    public void ValidateAnswer()
    {
        if (textInput.text == questionText.text)
        {
            StartNextLevel();
            return;
        }
        for (int i = 0; i < textInput.text.Length; i++)
        {
            if (textInput.text[i] != questionText.text[i])
            {
                Lost();
                return;
            }
        }
        
    }
    void Lost()
    {
        StartGameFromStart();
    }
    int GetRandomNumber()
    {
        return Random.Range(1, 10);
    }
}
