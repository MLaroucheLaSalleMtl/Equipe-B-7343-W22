using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MovingCube : MonoBehaviour
{
    [SerializeField] public GameObject instance;
    [SerializeField] GameObject initialCube;
    [SerializeField] Material mail;

    public static MovingCube currentCube { get; private set; }
    public static MovingCube lastCube { get; private set; }
    [SerializeField] public float speed { get; set; }
    public MoveDirection MoveDirection { get; set; }

    

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
        if (MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * speed;
        else
            transform.position += transform.right * Time.deltaTime * speed;
    }
    private void FixedUpdate()
    {
        initialCube.GetComponent<MovingCube>().speed = 0f;
        currentCube.speed = GameManager.increasedspeed;
    }
    public void Stop()
    {
        speed = 0;
        float hangover = GetHangover();


        float max = MoveDirection == MoveDirection.Z ? lastCube.transform.localScale.z : lastCube.transform.localScale.x;
        //Utilise soit le z ou le x pour calculer le hangover max selon la direction
        if (MathF.Abs(hangover) >= max)
        {
            //END GAME
            Interactions.endGame = true;
            GameObject[] MinigameUI;
            MinigameUI = GameObject.FindGameObjectsWithTag("Minigame");
            foreach(GameObject elements in MinigameUI)
            {
                elements.SetActive(false);
            }
            
            CloseMiniGame();
        }
        else
        {
            GameManager.score++;
        }

        float direction = hangover > 0 ? 1f : -1f; //Si le cube n'a pas encore passé le cube initial, retourne -1.
                                                   //Si le cube à dépassé le cube initial, retoune 1

        if (MoveDirection == MoveDirection.Z && Interactions.endGame == false)
        {
            SliceZ(hangover, direction);
        }
        else if(MoveDirection == MoveDirection.X && Interactions.endGame == false)
            SliceX(hangover, direction);


        lastCube = this;

    }

    public void HideScore()
    {
        
    }

    private float GetHangover()
    {
        if(MoveDirection == MoveDirection.Z) return transform.position.z - lastCube.transform.position.z;
        else return transform.position.x - lastCube.transform.position.x;
    }

    private void SliceX(float hangover,float direction)
    {
        if(MoveDirection == MoveDirection.X)
        {
            float newXSize = lastCube.transform.localScale.x - Mathf.Abs(hangover); //Size du cube restant
            float fallingBlockSize = transform.localScale.x - newXSize; //Size du cube qui tombe

            float newXPosition = lastCube.transform.position.x + (hangover / 2); // Changer la position pour rencentrer après le rescaling
            transform.localScale = new Vector3(newXSize, transform.localScale.y,transform.localScale.z);
            transform.position = new Vector3(newXPosition,transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (newXSize / 2f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

            SpawnFallingCube(fallingBlockXPosition, fallingBlockSize);
        }
    }
    private void SliceZ(float hangover, float direction)
    {
        if (MoveDirection == MoveDirection.Z)
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
    }

    private void SpawnFallingCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = mail;

        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z );
            cube.transform.position = new Vector3(fallingBlockZPosition,transform.position.y, transform.position.z );
        }

        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 1f);
    }

    public void CloseMiniGame()
    {
        GameObject.FindGameObjectWithTag("ScoreTXT").GetComponent<Text>().text = "YOU GOT " + Interactions.score + " Parcels";
        GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>().enabled = true;
        GameManager.increasedspeed = 1f;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject[] cubes;
        cubes = GameObject.FindGameObjectsWithTag("MovingCube");
        foreach(GameObject element in cubes)
        {
            Destroy(element);
        }
        lastCube = null;
        currentCube = null;
        Interactions.minigame = false;
        SceneManager.UnloadSceneAsync(1);
    }

}
