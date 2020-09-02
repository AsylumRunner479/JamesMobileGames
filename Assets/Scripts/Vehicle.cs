using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float topSpeed = 10;
    [SerializeField]
    private float turnSpeed = 1;

    private Rigidbody rb;
    private Vector3 velocity;
    private float speed = 0;
    private Gyroscope gyro;
    private float rotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        rotation += gyro.gravity.z;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.up);


    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        rb.AddForceAtPosition(new Vector3(0, 0, acceleration), transform.position, ForceMode.Impulse);
    }
}
