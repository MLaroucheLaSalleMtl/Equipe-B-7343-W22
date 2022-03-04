using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartMiniGame : MonoBehaviour
{
    [SerializeField] public GameObject miniGame;
    private void OnEnable()
    {
        Instantiate(miniGame);
    }
}
