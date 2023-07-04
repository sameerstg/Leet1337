using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChimpGame : MonoBehaviour
{

    public AudioClip auClip,auLostClip;
    public AudioSource auSource;
    public static ChimpGame _instance;
    public Color color;
    public GameObject mainPanel;
    public TextMeshProUGUI triesText;
    int tries;
    Dictionary<Vector2,TextMeshProUGUI> grid = new();
    public List<GameObject> tilesInSeries = new();
    public Event cellClicked;
    public int tileAmount;
    List<Vector2> tilesPos;
    bool isHidenAll;
    public GameObject resultPanel,gamePanel;
    public TextMeshProUGUI scoreText,highScoreText;
    public int highScore,score;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("ChimpGame");
        highScoreText.text = "High Score : " +  highScore.ToString();

        auSource = Camera.main.GetComponent<AudioSource>();
        
        for (int row = 0; row < 8; row++)
        {
            for (int coloumn = 0; coloumn < 8; coloumn++)
            {

                mainPanel.transform.GetChild(row).transform.GetChild(coloumn).GetComponent<Image>().color = color;
                
                grid.Add(new Vector2(row, coloumn), 
                    mainPanel.transform.GetChild(row).transform.GetChild(coloumn).GetChild(0).GetComponent<TextMeshProUGUI>());
            }
        }
        HideAllTiles();
        StartGame();
    }
    void StartGame()
    {
        score = 0;
        tries = 3;
        scoreText.text = "Score : "+score.ToString();

        triesText.text = $"Tries left = {tries}";
        tileAmount = 5;
        CreateTiles(tileAmount);

    }
    void CreateTiles(int amount)
    {
        isHidenAll = false;
        foreach (var item in grid)
        {
            item.Value.transform.parent.gameObject.SetActive(false);
        }
        tilesPos = new();
        Vector2 newTile;
        for (int i = 0; i < amount; i++)
        {
            
            newTile = new Vector2(Random.Range(0, 8), Random.Range(0, 8));
            if (tilesPos.Contains(newTile))
            {
                i--;
                continue;
            }
            else
            {
                tilesPos.Add(newTile);
            }
            

        }
        int ii = 1;
        tilesInSeries = new();
        foreach (var item in tilesPos)
        {

            RevealTile(grid[item].gameObject.transform.parent.gameObject);
            grid[item].text = ii.ToString();
            ii += 1;
        }
    }
    public void CheckCell(GameObject tile)
    {
        if (tilesInSeries[0] == tile)
        {
            auSource.PlayOneShot(auClip);

            tilesInSeries.Remove(tile);
            tilesPos.Remove(tilesPos[0]);
            HideTileProper(tile);
            if (!isHidenAll)
            {
                foreach (var item in tilesPos)
                {
                    grid[item].text = "";
                }
            }
        }
        else
        {
            auSource.PlayOneShot(auLostClip);

            Lost(tileAmount);
        }
        if (tilesInSeries.Count == 0)
        {

            tileAmount += 1;
            score++;
            if (highScore<score)
            {
                highScore = score;
            PlayerPrefs.SetInt("ChimpGame",score);

            highScoreText.text = "High Score : " + highScore.ToString();
            }
            scoreText.text = "Score : " +  score.ToString();
            CreateTiles(tileAmount);
        }
    }
    void HideAllTiles()
    {
        foreach (var item in grid)
        {
            item.Value.transform.parent.gameObject.SetActive(false);
        }

    }
    public void RevealTile(GameObject tile)
    {
        tilesInSeries.Add(tile);
        tile.SetActive(true);

        tile.SetActive(true);
    }
    public void HideTileProper(GameObject tile)
    {
        tile.SetActive(false);
    }
    public void Lost(int amount)
    {
        tries -= 1;
        triesText.text = $"Tries left = {tries}";
        if (tries!=0)
        {
           CreateTiles(tileAmount);
        }
        else
        {
            StartGame();
            //StartCoroutine(ShowScore());
        }
    }
    //IEnumerator ShowScore()
    //{
    //    gamePanel.SetActive(false);
    //    resultPanel.SetActive(true);
    //    score.text = $"Score: {tileAmount}";
    //    yield return new WaitForSeconds(5);
    //    gamePanel.SetActive(true);
    //    resultPanel.SetActive(false);
    //    StartGame();
    //}
}
