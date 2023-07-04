using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualMemoryGame : MonoBehaviour
{
    public AudioClip auClip, auLostClip;
    AudioSource auSource;
    public static VisualMemoryGame _instance;
    public GameObject mainPanel,imgPrefab;
    public Color32 deselectColor, selectColor,clearColor;
    GridLayoutGroup gridLayout;
    public List<Image> gridImg = new List<Image>();
    List<int> tileQueue = new();
    public bool touchPermit;
    int level,prevFab,fab;
    int boxAmount;
    public TextMeshProUGUI scoreText, highScoreText;
    public int highScore;
    public List<Button> buttons = new();
    private int score;

    private void Awake()
    {
        _instance = this;
        gridLayout = mainPanel.GetComponent<GridLayoutGroup>();
        auSource = Camera.main.GetComponent<AudioSource>();

    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Visual");
        highScoreText.text = "High Score : " + highScore;
        
        StartNewGame();
    }
    void SetGrid()
    {
        gridImg = new List<Image>();

        buttons.Clear();
        List<GameObject> tempGO = new List<GameObject>();
        for (int i = 0; i < mainPanel.transform.childCount; i++)
        {
            tempGO.Add(mainPanel.transform.GetChild(i).gameObject);
        }
        foreach (var item in tempGO)
        {
            Destroy(item);
        }
        gridLayout.cellSize = new Vector2((1000 / level) - 10, (1000 / level) - 10);
        for (int i = 0; i < Mathf.Pow(level, 2); i++)
        {
            gridImg.Add(Instantiate(imgPrefab,mainPanel.transform).GetComponent<Image>());
            var butt = gridImg[i].gameObject.AddComponent<ButtonClickedVisualSequence>();

            buttons.Add(butt.button);

        }
        ChangeAllTimeColor(clearColor);
        NewGame();

    }
    void ChangeAllTimeColor(Color32 color)
    {
        foreach (var item in gridImg)
        {
            item.color = color;
        }
    }
    void StartNewGame()
    {
        score = 0;
        scoreText.text = "Score : "+score;
        
        prevFab = 3;
        fab = 5;
        level = 3;
        boxAmount = 3;
        SetGrid();
    }

    void NewGame()
    {
        touchPermit = false;

               
        if (boxAmount==fab)
        {
            int temp = fab;
            fab += prevFab;
            prevFab = fab;
            level += 1;
            SetGrid();
            return;
        }

        scoreText.text = "Score : " + score;

        if (highScore < score)
        {
            PlayerPrefs.SetInt("Visual", score);

            highScore = score;

            highScoreText.text = "High Score : " + highScore;

        }
        score++;
       
        tileQueue = new List<int>();
        for (int i = 0; i < boxAmount; i++)
        {
            int ranTileIndex = Random.Range(0, gridImg.Count);
            while (tileQueue.Contains(ranTileIndex))
            {
                ranTileIndex = Random.Range(0, gridImg.Count);
            }
                tileQueue.Add(ranTileIndex);
        }
        StartCoroutine(HighlightAllTiles());
        boxAmount += 1;
    }
    int GetNewTileToQueue()
    {
        return Random.Range(0, (int)Mathf.Pow(level, 2)+1);
    }
    IEnumerator HighlightTile(int index)
    {
        yield return new WaitForSeconds(.05f);
        
        gridImg[index].color = selectColor;

        yield return new WaitForSeconds(.08f);
        gridImg[index].color = clearColor;
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        if (tileQueue.Count == 0)
        {

            NewGame();

        }
    }

    IEnumerator HighlightAllTiles()
    {

        foreach (var item in buttons)
        {
            item.interactable = false;
        }
        foreach (var item in gridImg)
        {
            item.color = clearColor;
        }
        yield return new WaitForSeconds(1f);
        foreach (var item in gridImg)
        {
            item.color = deselectColor;
        }
        yield return new WaitForSeconds(.1f);

        foreach (var item in tileQueue)
        {
            gridImg[int.Parse(item.ToString())].color = selectColor;            
        }
        yield return new WaitForSeconds(1.5f);

        foreach (var item in tileQueue)
        {
            gridImg[int.Parse(item.ToString())].color = deselectColor;

        }
        touchPermit = true;
        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }

    public void CheckCell(Image image)
    {
        if (image.color == clearColor)
        {
            return;
        }
        for (int i = 0; i < gridImg.Count; i++)
        {
            if (image == gridImg[i] )
            {
                if (tileQueue.Contains(i))
                {
                    tileQueue.Remove(i);
                    CheckLevelCompletion();
                    auSource.PlayOneShot(auClip);

                    StartCoroutine(HighlightTile(i));

                    return;
                }
                

            }

        }
        auSource.PlayOneShot(auLostClip);
            StartNewGame();

        
        
        
    }
}
