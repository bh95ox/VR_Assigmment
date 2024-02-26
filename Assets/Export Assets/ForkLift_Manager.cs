using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLift_Manager : MonoBehaviour
{
    [SerializeField] private GameObject FrontLeft_Wheel;
    [SerializeField] private GameObject FrontRight_Wheel;
    [SerializeField] private GameObject BackLeft_Wheel;
    [SerializeField] private GameObject BackRight_Wheel;
    [SerializeField] private GameObject Fork_Lifter;
    [SerializeField] private Transform ForkMaxHeight;
    [SerializeField] private Transform ForkMinHeight;
    [SerializeField, Range(10f, 35f)] private float MaxWheels_Rotation;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float Fork_Movement_Speed;
    [SerializeField] private float Max_Speed;

    public float movementSpeed = 5f;
    public float rotationSpeed = 50f;
    public float forkLiftSpeed = 2f;
    public float forkLiftHeight = 3f;

    public Transform Fork;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move forward and backward
        transform.Translate(Vector3.forward * vertical * movementSpeed * Time.deltaTime);
        if(vertical != 0)
        {
            // Rotate left and right
            transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);
        }

        // Lift and lower forks
        if (Input.GetKey(KeyCode.Q))
        {
            LiftForks();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            LowerForks();
        }
    }

    void LiftForks()
    {
        // Move the forks upward
        Fork.Translate(Vector3.up * forkLiftSpeed * Time.deltaTime);

        // Clamp the fork height to a maximum value
        float clampedHeight = Mathf.Clamp(Fork.localPosition.y, 0f, forkLiftHeight);
        Fork.localPosition = new Vector3(Fork.localPosition.x, clampedHeight, Fork.localPosition.z);
    }

    void LowerForks()
    {
        // Move the forks downward
        Fork.Translate(Vector3.down * forkLiftSpeed * Time.deltaTime);

        // Clamp the fork height to a minimum value
        float clampedHeight = Mathf.Clamp(Fork.localPosition.y, 0f, forkLiftHeight);
        Fork.localPosition = new Vector3(Fork.localPosition.x, clampedHeight, Fork.localPosition.z);

    }






}




