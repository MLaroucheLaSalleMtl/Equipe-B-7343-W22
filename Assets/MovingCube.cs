using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    [SerializeField] GameObject initialCube;
    [SerializeField] Material mail;
    public static MovingCube currentCube { get; private set; }
    public static MovingCube lastCube { get; private set; }
    [SerializeField] public float speed { get; set; } = 1f;

    private void OnEnable()
    {
        if (lastCube == null)
            lastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        currentCube = this;

        transform.localScale = new Vector3(lastCube.transform.localScale.x, transform.localScale.y, lastCube.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        initialCube.GetComponent<MovingCube>().speed = 0f;
    }
    public void Stop()
    {
        speed = 0;
        float hangover = transform.position.z - lastCube.transform.position.z;

        if (MathF.Abs(hangover) >= lastCube.transform.localScale.z)
        {
            //END GAME
            lastCube = null;
            currentCube = null;
            SceneManager.LoadScene(2);
        }

        float direction = hangover > 0 ? 1f : -1f; //Si le cube n'a pas encore passé le cube initial, retourne -1.
                                                   //Si le cube à dépassé le cube initial, retoune 1
        SliceZ(hangover,direction);
        lastCube = this;
        
    }

    private void SliceZ(float hangover,float direction)
    {
        float newZSize = lastCube.transform.localScale.z - Mathf.Abs(hangover); //Size du cube restant
        float fallingBlockSize = transform.localScale.z - newZSize; //Size du cube qui tombe

        float newZPosition = lastCube.transform.position.z + (hangover / 2); // Changer la position pour rencentrer après le rescaling
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnFallingCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnFallingCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = mail;
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 1f);
    }
}
