using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is pretty much ripped as is from something I found online, which I'm not sure I like...

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        Vector3 velocity = targetPos - transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, moveSpeed * Time.deltaTime);
    }
}
