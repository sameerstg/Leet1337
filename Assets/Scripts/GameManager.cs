using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadGame(string name)
    {
        StartCoroutine(DelayLoad(name));
    }
    IEnumerator DelayLoad(string name)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(name);

    }

}
