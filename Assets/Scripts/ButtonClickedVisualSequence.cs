using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickedVisualSequence : MonoBehaviour
{
    VisualMemoryGame sequenceGame;
    Image image;
    public Button button;
    private void Awake()
    {
        image = GetComponent<Image>();
        button = gameObject.AddComponent<Button>();
        button.transition = Selectable.Transition.None;
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
