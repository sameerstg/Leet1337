using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberGame : MonoBehaviour
{
    public AudioClip auClip;
    public AudioSource auSource;
    public static NumberGame _instance;
    public TextMeshProUGUI questionText, highScoreText,scoreText;
    public TMP_InputField textInput;
    WidthAnimation widthAnim;
    int highScore;
    int level;
    private void Awake()
    {
        _instance = this;
        auSource = Camera.main.GetComponent<AudioSource>();
        widthAnim = textInput.GetComponent<WidthAnimation>();
        highScore = PlayerPrefs.GetInt("NumberGame");
        highScoreText.text = $"High Score : {highScore}";
        scoreText.text = $"Score : {level}";
    }
    private void Start()
    {
        StartGameFromStart();
    }
    private void StartGameFromStart()
    {
        widthAnim.timeData = 1f;
        level = 1;
        SetNewQuestion();
    }
    void StartNextLevel()
    {
        
        level += 1;
        scoreText.text = $"Score : {level}";

        if (level>highScore)
        {
            highScore = level;
            highScoreText.text = $"High Score : {highScore}";
            PlayerPrefs.SetInt("NumberGame", highScore);
        }
        //if (widthAnim.timeData != 1)
        //{
        //    widthAnim.timeData += 0.35f;

        //}
        //else
        //{
        //    widthAnim.timeData += 0.15f;
        //}
        Debug.Log(level);

        //if (level <= 3)
        //{
        //    widthAnim.timeData = 1;
        //}
        //else
        //{

            widthAnim.timeData = (int)(level/3)+1;
            Debug.Log(widthAnim.timeData);

        //}
        //widthAnim.timeData  = Mathf.Ceil(level/3);

        SetNewQuestion();
    }
    void SetNewQuestion()
    {
        questionText.gameObject.SetActive(true);
        textInput.text = "";
        questionText.text = "";
        for (int i = 0; i < level; i++)
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
