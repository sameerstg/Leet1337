using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceGame: MonoBehaviour
{
    public static SequenceGame _instance;
    public GameObject mainPanel;
    public Color32 deselectColor, selectColor,clearColor;
    public List<Image> gridImg = new List<Image>();
    Queue tileQueue,checkTileQueue;
    public bool touchPermit;
    int level;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        tileQueue = new();
        checkTileQueue = new();
        for (int i = 0; i < mainPanel.transform.childCount; i++)
        {
            for (int j = 0; j < mainPanel.transform.GetChild(i).childCount; j++)
            {
                gridImg.Add(mainPanel.transform.GetChild(i).GetChild(j).GetComponent<Image>());
                gridImg[gridImg.Count - 1].color = deselectColor;
            }
        }
        StartNewGame();
    }
    void StartNewGame()
    {
        tileQueue = new();
        checkTileQueue = new();

        AddTile();
    }

    void AddTile()
    {
        touchPermit = false;
        tileQueue.Enqueue(Random.Range(0, gridImg.Count));
        StartCoroutine(HighlightAllTiles());
    }

    IEnumerator HighlightTile(int index)
    {
        yield return new WaitForSeconds(.02f);
        
        gridImg[index].color = selectColor;

        yield return new WaitForSeconds(.08f);
        gridImg[index].color = deselectColor;
        CheckLevelCompletion();
    }
    void CheckLevelCompletion()
    {
        if (checkTileQueue.Count == 0)
        {
            touchPermit = false;
            AddTile();
        }
        
    }
    IEnumerator HighlightAllTiles()
    {
        foreach (var item in gridImg)
        {
            item.color = clearColor;
        }
        yield return new WaitForSeconds(1f);
        foreach (var item in gridImg)
        {
            item.color = deselectColor;
        }

        foreach (var item in tileQueue)
        {
            yield return new WaitForSeconds(.1f);

            gridImg[int.Parse(item.ToString())].color = selectColor;

            yield return new WaitForSeconds(0.5f);
            gridImg[int.Parse(item.ToString())].color = deselectColor;
        }
        checkTileQueue = new(tileQueue);
        touchPermit = true;
    }

    public void CheckCell(Image image)
    {
        for (int i = 0; i < gridImg.Count; i++)
        {
            if (image == gridImg[i])
            {
                if (int.Parse(checkTileQueue.Peek().ToString())==i)
                {
                    StartCoroutine(HighlightTile(i));
                    checkTileQueue.Dequeue(); 
                }
                else
                {
                    print("failed");
                    StartNewGame();
                }

            }
        }

                return;
        
    }
}
