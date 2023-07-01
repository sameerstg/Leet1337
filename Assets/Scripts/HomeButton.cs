using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void Click()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(0);

    }
}
