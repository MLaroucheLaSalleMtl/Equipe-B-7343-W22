using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
    public void Play_btn()
    {
        SceneManager.LoadScene(1);
    }
}
