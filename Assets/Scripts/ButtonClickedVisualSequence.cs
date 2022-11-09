using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickedVisualSequence : MonoBehaviour
{
    VisualMemoryGame sequenceGame;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(Clicked);
    }
    private void Start()
    {
        sequenceGame = VisualMemoryGame._instance;
    }
    public void Clicked()
    {
        if (sequenceGame.touchPermit)
        {
            sequenceGame.CheckCell(image);

        }
    }



}
