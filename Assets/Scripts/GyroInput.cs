using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroInput : MonoBehaviour
{
    private Gyroscope gyro;
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
        }
        else
        {
            enabled = false;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            gyro.enabled = true;

            transform.position += gyro.attitude.eulerAngles;
            Debug.Log(gyro.attitude);
        }
        else if (gyro.enabled)
        {
            gyro.enabled = false;
        }
    }
}
