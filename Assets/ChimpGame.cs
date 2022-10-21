using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChimpGame : MonoBehaviour
{

    public static ChimpGame _instance;
    public Color color;
    public GameObject mainPanel;
    Dictionary<Vector2,TextMeshProUGUI> grid = new Dictionary<Vector2, TextMeshProUGUI>();
    public List<Vector2> rowColoumn = new List<Vector2>();
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    public List<GameObject> tilesInSeries = new();
    public Event cellClicked;
    public int tileAmount;
    List<Vector2> tilesPos;
    bool isHidenAll;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int coloumn = 0; coloumn < 8; coloumn++)
            {

                mainPanel.transform.GetChild(row).transform.GetChild(coloumn).GetComponent<Image>().color = color;
                
                grid.Add(new Vector2(row, coloumn), 
                    mainPanel.transform.GetChild(row).transform.GetChild(coloumn).GetChild(0).GetComponent<TextMeshProUGUI>());
            }
        }
        foreach (var item in grid)
        {
            item.Value.transform.parent.gameObject.SetActive(false);
        }
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
            Lost(tileAmount);
        }
        if (tilesInSeries.Count == 0)
        {
            print("you win");
            tileAmount += 1;
            CreateTiles(tileAmount);
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
        print("Print You lost");
        CreateTiles(tileAmount);

    }
}
