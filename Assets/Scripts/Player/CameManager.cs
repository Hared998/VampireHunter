using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.one;
    public DiscoverMap dm;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition,ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
