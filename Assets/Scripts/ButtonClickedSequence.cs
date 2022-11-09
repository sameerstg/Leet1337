using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickedSequence : MonoBehaviour
{
    SequenceGame sequenceGame;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(Clicked);
        button.transition = Selectable.Transition.None;  
    }
    private void Start()
    {
        sequenceGame = SequenceGame._instance;
    }
    public void Clicked()
    {
        if (sequenceGame.touchPermit)
        {
            sequenceGame.CheckCell(image);

        }










    }



}
