using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem particles;
    public Transform raycastOrigin;
    public GameObject bulletPrefab;
    Vector3 targetPosition;

    Ray ray;
    RaycastHit hitInfo;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AimAt(Vector3 target) {
        targetPosition = target;
        raycastOrigin.LookAt(target);
    }

    public void Fire() {
        particles.Emit(1);

        Instantiate(
            bulletPrefab, 
            raycastOrigin.position, 
            raycastOrigin.rotation
        );
    }
}
