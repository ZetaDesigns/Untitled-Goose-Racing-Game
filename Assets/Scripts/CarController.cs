using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;

    private bool honk;
    public AudioSource audiosrc;
    public List<AudioClip> honkSounds = new List<AudioClip>();
    public WheelCollider[] frontColliders;
    public WheelCollider[] backColliders;

    public Transform[] frontWheels;
    public Transform[] backWheels;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    public float brakeForce = 5000;
    private float brake = 0;

    public GameObject centerOfMass;

    System.Random rand = new System.Random();

    private void Start()
    {
        this.GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.2f, 0f);

    }
    void FixedUpdate()
    {
        GetInput();
        if(honk)
        {
            audiosrc.clip = honkSounds[rand.Next(honkSounds.Count)];
            audiosrc.Play();
        }
        Steer();
        Accelerate();
        brake = 0;
        if (Input.GetKey(KeyCode.Space) == true)
        {
            brake = brakeForce;
        }
        frontColliders[0].brakeTorque = brake;
        frontColliders[1].brakeTorque = brake;
        WheelPoses();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        honk = Input.GetKeyDown(KeyCode.E);
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontColliders[0].steerAngle = steerAngle;
        frontColliders[1].steerAngle = steerAngle;

    }

    private void Accelerate()
    {
        frontColliders[0].motorTorque = verticalInput * motorForce;
        frontColliders[1].motorTorque = verticalInput * motorForce;

    }
    private void WheelPoses()
    {
        UpdateWheelPose(frontColliders[0], frontWheels[0]);
        UpdateWheelPose(frontColliders[1], frontWheels[1]);

        UpdateWheelPose(backColliders[0], backWheels[0]);
        UpdateWheelPose(backColliders[1], backWheels[1]);

    }

    private void UpdateWheelPose(WheelCollider col, Transform trans)
    {
        Vector3 pos = trans.position;
        Quaternion quat = trans.rotation;

        col.GetWorldPose(out pos, out quat);
        quat = quat * Quaternion.Euler(new Vector3(90, 90, 180));
        trans.position = pos;
        trans.rotation = quat;
    }
}
