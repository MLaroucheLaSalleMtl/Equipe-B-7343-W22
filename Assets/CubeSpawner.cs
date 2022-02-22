using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);
        if (MovingCube.lastCube != null && MovingCube.lastCube != GameObject.Find("Start"))
        {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.lastCube.transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
            cube.GetComponent<MovingCube>().speed = 1f;
        }
        else
        {
            cube.transform.position = transform.position;
        }
        
    }
}
