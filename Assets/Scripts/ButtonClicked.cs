using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    ChimpGame chimpGame;
    private void Awake()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(Clicked);
    }
    private void Start()
    {
        chimpGame = ChimpGame._instance;
    }
    public void Clicked()
    {
        chimpGame.CheckCell(this.gameObject);
    }



}
