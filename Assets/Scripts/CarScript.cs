using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField] private Transform[] DestinationPoints;
    private int currentPoint;
    public WheelCollider FR;
    public WheelCollider FL;
    [SerializeField] public float maxMotorTorque = 40f;
    public float currentSpeed;
    public float maxSpeed = 5f;
    public float maxSteerAngle = 45f;
  
    // Update is called once per frame
    void FixedUpdate()
    {

            Vector3 relativeVector = transform.InverseTransformPoint(DestinationPoints[currentPoint].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            FR.steerAngle = newSteer;
            FL.steerAngle = newSteer;
            FR.motorTorque = 0;
            FL.motorTorque = 0;
        

        currentSpeed = 2 * Mathf.PI * FL.radius * FL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && currentSpeed > 0)
        {
            FR.motorTorque = maxMotorTorque;
            FL.motorTorque = maxMotorTorque;
        }
        else if(currentSpeed<=0)
        {
            FR.motorTorque = 60;
            FL.motorTorque = 60;
        }
        else
        {
            FR.motorTorque = 0;
            FL.motorTorque = 0;
        }

        if (Vector3.Distance(transform.position, DestinationPoints[currentPoint].position) < 1f)
        {
            if (currentPoint == DestinationPoints.Length - 1)
                currentPoint = 0;
            else
            {
                    currentPoint++;
            }
        }
        Debug.Log(DestinationPoints[currentPoint].name);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Joe")
        {
            if (currentSpeed >= 1 && currentSpeed < 10)
            {
                collision.gameObject.GetComponentInParent<PlayerManager>().Health -= currentSpeed;
            }
            if (currentSpeed >= 10)
                collision.gameObject.GetComponentInParent<Interactions>().FailDelivery();
        }
    }
}
