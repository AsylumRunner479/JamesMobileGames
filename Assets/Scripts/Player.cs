using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * JoyStickInput.axis.y * moveSpeed;
        transform.position += transform.right * Time.deltaTime * JoyStickInput.axis.x * moveSpeed;

    }
}
