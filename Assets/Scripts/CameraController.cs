using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothTime = 1;
    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = target.position + CalcDesiredOffset();
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothTime);

        transform.position = smoothedPos;
        transform.LookAt(target);
    }
    private Vector3 CalcDesiredOffset()
    {
        Vector3 desired = Vector3.zero;
        desired += target.right * offset.x;
        desired += target.up * offset.y;
        desired += target.forward * offset.z;
        return desired;
    }
}
