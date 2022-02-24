using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score;
    [SerializeField] MovingCube firstCube;
    public static GameManager instance = null;
    private CubeSpawner[] spawners;
    private int spawnerindex = 1;
    private CubeSpawner currentSpawner;

    public static float increasedspeed =1f;

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
        firstCube.MoveDirection = MoveDirection.Z;
        firstCube.gameObject.SetActive(true);

        spawners = FindObjectsOfType<CubeSpawner>();
    }
    private void Update()
    {
        
    }
    public void OnStopCube()
    {
        if(MovingCube.currentCube != null)
        MovingCube.currentCube.Stop();

        spawnerindex = spawnerindex == 0 ? 1 : 0;
        currentSpawner = spawners[spawnerindex];

        increasedspeed += 0.3f;
        currentSpawner.SpawnCube();
    }

}
