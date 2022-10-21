using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculationGame : MonoBehaviour
{

    [Header("Choice Things")]
    public List<TMP_InputField> minMaxInputs = new List<TMP_InputField>();
    public GameObject buttonToStart;
    [Header("......................................................................................")]

    [Header("Game Things")]
    public TextMeshProUGUI questionText;
    public TMP_InputField answerText;
    public float minToAdd, maxToAdd;
    public float minToMultiply, maxToMultiply;
    public string question, answer;
    int score;
    public float time;
    bool isStarted;
    public TextMeshProUGUI timeText,scoreText;



    public GameObject choicePanel, gamePanel;

    [Header("Result")]
    public GameObject resultPanel;
    public TextMeshProUGUI scoreReuslt, timeResult;
    enum Operants
    {
        add,subtract,multiply,divide
    }




    public void CheckAllMinMax()
    {
        foreach (var item in minMaxInputs)
        {
            if (item.text.Length<=0 || int.Parse(item.text)<=1)
            {
                if (buttonToStart.activeInHierarchy)
                {
                    buttonToStart.SetActive(false);
                }
                return;
            }
        }
        minToAdd = int.Parse(minMaxInputs[0].text);
        maxToAdd = int.Parse(minMaxInputs[1].text);
        minToMultiply= int.Parse(minMaxInputs[2].text);
        maxToMultiply = int.Parse(minMaxInputs[3].text);
        buttonToStart.SetActive(true);
    }
    private void Start()
    {
        CheckAllMinMax();
        StartGame();
    }
    public void StartGame()
    {

        //  Game Starting Settings

        score = 0;
        answerText.text = "";
        answerText.ActivateInputField();
        GenerateQuestion();
        isStarted = true;
        timeText.text = $"Time Passed = ";
        scoreText.text = $"Score = 0";
        time = 0;
    }
    private void Update()
    {
        if (isStarted)
        {
            time += Time.deltaTime;
            timeText.text = $"Time = {(int)time}";
        }
        if (time>120)
        {
            time = 120;
            StartCoroutine(ShowResult());
        }
    }
    void GenerateQuestion()
    {
        int op = Random.Range(0, 4);
        int num1, num2;
        switch (op)
        {
            case 0:
                num1 = (int)(Random.Range(2, minToAdd + 1)); 
                num2 = (int)(Random.Range(2, maxToAdd + 1));
                answer = (num1 + num2).ToString();
                question = $"{num1} + {num2} = ";
                break;
            case 1:
                num1 = (int)(Random.Range(2, minToAdd + 1)); 
                num2 = (int)(Random.Range(2, maxToAdd + 1));
                answer = (num2 - num1).ToString();
                question = $"{num2} - {num1} = ";
                break;
            case 2:
                num1 = (int)(Random.Range(2, minToMultiply+1)); 
                num2 = (int)(Random.Range(2, maxToMultiply + 1));
                answer = (num1 * num2).ToString();
                question = $"{num1} * {num2} = ";
                break;
            case 3:
                num2 = (int)(Random.Range(2, maxToMultiply + 1));
                List<int> num1s = new List<int>();
                for (int i = 2; i < num2/2; i++)
                {
                    if (num2%i==0)
                    {
                        num1s.Add(i);
                    }
                }
                if (num1s.Count > 0)
                {
                    num1 = num1s[Random.Range(0, num1s.Count)];
                }
                else
                {
                    GenerateQuestion();
                    num1 = 1;
                }
                answer = (num2 / num1).ToString();
                question = $"{num2} / {num1} = ";
                break;
            default:
                break;
        }
        ShowQuestion();
    }
    void ShowQuestion()
    {
        questionText.text = question;
    }
    public void ValidateAnswer()
    {

        if (answer == answerText.text)
        {
            score += 1;
            scoreText.text = $"Score = {score}";
            answerText.text = "";
            GenerateQuestion();
        }
        
    }



    IEnumerator ShowResult()
    {

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);
        isStarted = false;

        scoreReuslt.text = $"Score = {score}";
        timeResult.text = $"In Time = {time}";
        yield return new WaitForSeconds(5f);
        resultPanel.SetActive(false);
        choicePanel.SetActive(true);

    }
}
