using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    int sceneInd;
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Nice to see you");
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneInd + 1);
    }
}
