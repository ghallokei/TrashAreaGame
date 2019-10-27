using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    
    public float smoothing = 2;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }
    void Update()
    {
        Vector3 newPos= player.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothing);

    }
}
