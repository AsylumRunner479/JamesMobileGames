using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    public Vector2 flick;
    [SerializeField]
    private Rigidbody cube;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                flick = (touch.position - touch.rawPosition).normalized;
                cube.AddForce(new Vector3(flick.x, 1, flick.y) * 5, ForceMode.VelocityChange);
            }
        }
    }
}
