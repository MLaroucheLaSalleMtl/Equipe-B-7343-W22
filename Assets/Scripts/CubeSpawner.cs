using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;
    [SerializeField] private MoveDirection movedirection;
    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);
        if (MovingCube.lastCube != null && MovingCube.lastCube != GameObject.Find("Start"))
        {
            //Fait apparaitre le cube à la même position en X ou en Z que le cube précédant selon le movedirection actuel afin d'aligner les cubes
            float x = movedirection == MoveDirection.X ? transform.position.x : MovingCube.lastCube.transform.position.x;
            float z = movedirection == MoveDirection.Z ? transform.position.z : MovingCube.lastCube.transform.position.z;

            cube.transform.position = new Vector3(x, MovingCube.lastCube.transform.position.y + cubePrefab.transform.localScale.y, z);
            cube.GetComponent<MovingCube>().speed = 1f;
        }
        else
        {
            cube.transform.position = transform.position;
        }
        cube.MoveDirection = movedirection;
    }
}
