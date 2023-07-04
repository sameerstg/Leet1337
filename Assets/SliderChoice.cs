using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChoice : MonoBehaviour
{
    public Slider min, max;
    public TextMeshProUGUI minText, maxText;
    AudioSource auSource;
    public AudioClip auClip;
    private void Awake()
    {
        auSource = Camera.main.GetComponent<AudioSource>();
        minText = min.GetComponentInChildren<TextMeshProUGUI>();
        maxText = max.GetComponentInChildren<TextMeshProUGUI>();
        min.value = 1;
        max.value = 2;
        minText.text = "Min : " + min.value.ToString();
        maxText.text = "Max : " + max.value.ToString();

        min.onValueChanged.AddListener(delegate {
            auSource.PlayOneShot(auClip);
            minText.text ="Min : "+ min.value.ToString();
            if (min.value>max.value)
            {
                max.value = min.value;
                maxText.text = "Max : " + min.value.ToString();
            }
            
            });
        max.onValueChanged.AddListener(delegate {
            auSource.PlayOneShot(auClip);

            maxText.text = "Max : " + max.value.ToString();
            if (min.value > max.value)
            {
                min.value = max.value;
                minText.text = "Min : " + max.value.ToString();
            }

        });

    }
}
