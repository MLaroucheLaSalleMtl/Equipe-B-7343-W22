using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
    }
    public void OnStopCube()
    {
        if(MovingCube.currentCube != null)
        MovingCube.currentCube.Stop();

        FindObjectOfType<CubeSpawner>().SpawnCube();
    }

}
