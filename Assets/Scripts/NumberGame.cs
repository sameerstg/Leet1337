using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberGame : MonoBehaviour
{
    public AudioClip auClip;
    public AudioSource auSource;
    public static NumberGame _instance;
    public TextMeshProUGUI questionText;
    public TMP_InputField textInput;
    WidthAnimation widthAnim;
    int level;
    private void Awake()
    {
        _instance = this;
        auSource = Camera.main.GetComponent<AudioSource>();
        widthAnim = textInput.GetComponent<WidthAnimation>();
    }
    private void Start()
    {
        StartGameFromStart();
    }
    private void StartGameFromStart()
    {
        widthAnim.timeData = 0.7f;
        level = 0;
        SetNewQuestion();
    }
    void StartNextLevel()
    {

        level += 1;
        widthAnim.timeData += 0.35f;

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
        auSource.PlayOneShot(auClip);
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
