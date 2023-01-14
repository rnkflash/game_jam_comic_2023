using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform followTarget;
    private float speed = 1.0f;
    public float maxSpeed = 9.0f;
    public float maxDistance = 50.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (followTarget != null) {
            transform.position = Vector3.Lerp (
                transform.position, 
                new Vector3(followTarget.position.x, transform.position.y, transform.position.z), 
                Time.deltaTime * speed
            );
            var newSpeed = 1.0f + maxSpeed * Mathf.Clamp(followTarget.position.x - transform.position.x, 0, maxDistance)/maxDistance;
            speed = Mathf.Clamp(newSpeed, 1.0f, 1.0f + maxSpeed);
        }
    }
}
