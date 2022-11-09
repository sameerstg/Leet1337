using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WidthAnimation : MonoBehaviour
{
    RectTransform rectTransform;
    TMP_InputField textInput;
    
    float timeData;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textInput = GetComponent<TMP_InputField>();
        timeData = 0.5f;
    }
        private void OnEnable()
    {
        
        textInput.interactable = true;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(Anim());

    }
    private IEnumerator Anim()
    {
        textInput.DeactivateInputField();

        rectTransform.sizeDelta = new Vector2(1, rectTransform.sizeDelta.y);

        while (rectTransform.sizeDelta.x != 1000)
        {

            yield return new WaitForSeconds(0.01f);
            rectTransform.sizeDelta = new Vector2(Mathf.MoveTowards(rectTransform.sizeDelta.x,1000, 1000*0.01f/timeData), rectTransform.sizeDelta.y);

        }

        timeData += 0.5f;


        NumberGame._instance.questionText.gameObject.SetActive(false);
        textInput.ActivateInputField();

    }
    public void OnActivate()
    {
        NumberGame._instance.questionText.gameObject.SetActive(false);
        
    }
}
