using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float time;
    Vector3 initialPosition;
    Vector3 initialVelocity;
    TrailRenderer tracer;
    float speed = 100.0f;
    float drop = 0.0f;
    float maxLifeTime = 3.0f;
    public TrailRenderer bulletTracerPrefab;
    public GameObject bulletGraphics;

    Ray ray;
    int raycastLayerMask;
    string[] shootableLayers = { "NavMeshLayer", "Units" };
    string[] shootableTags = { "UnitBodyPart", "Ground" };

    void Start()
    {
        Vector3 position = transform.position;
        Vector3 velocity = transform.forward * speed;

        initialPosition = position;
        initialVelocity = velocity;
        time = 0.0f;
        tracer = Instantiate(bulletTracerPrefab, position, Quaternion.identity);
        tracer.AddPosition(position);

        raycastLayerMask = LayerMask.GetMask(shootableLayers);
    }

    void Update()
    {
        Vector3 p0 = GetPosition();
        time += Time.deltaTime;
        Vector3 p1 = GetPosition();

        bulletGraphics.transform.position = p1;

        RaycastSegment(p0, p1);

        if (time >= maxLifeTime)
            Destroy(this.gameObject);
    }

    void RaycastSegment(Vector3 start, Vector3 end) {
        Vector3 direction = (end - start);
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        Debug.DrawLine(ray.origin, ray.origin + ray.direction.normalized * distance, Color.red);

        RaycastHit[] hits = System.Array.FindAll(Physics.RaycastAll(ray.origin, ray.direction, distance, raycastLayerMask), h => shootableTags.Contains(h.collider.tag));

        if (hits.Length > 0) {
            var hitInfo = hits[0];
            if (hitInfo.transform.tag == "UnitBodyPart")
                ParticleManager.Instance.HitEffect(hitInfo.point, hitInfo.normal);
            else
                ParticleManager.Instance.HitMetalEffect(hitInfo.point, hitInfo.normal);
            tracer.transform.position = hitInfo.point;
            time = maxLifeTime;
        } else {
            tracer.transform.position = end;
        }
    }

    Vector3 GetPosition() {
        Vector3 gravity = Vector3.down * drop;
        return (initialPosition) + (initialVelocity*time) + (0.5f*gravity*time*time);
    }

}
