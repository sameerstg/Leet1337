using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WidthAnimation : MonoBehaviour
{
    RectTransform rectTransform;
    TMP_InputField textInput;
    
   public float timeData;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textInput = GetComponent<TMP_InputField>();
        timeData = 1f;
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
        float time = 0;
        while (time <timeData)
        {
            time += Time.deltaTime;
            rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0,1000,time/timeData), rectTransform.sizeDelta.y);
            yield return null;

        }

        Debug.Log(time);


        NumberGame._instance.questionText.gameObject.SetActive(false);
        textInput.ActivateInputField();

    }
    public void OnActivate()
    {
        NumberGame._instance.questionText.gameObject.SetActive(false);
        
    }
}
