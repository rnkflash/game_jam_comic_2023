using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager _instance;
    public static ParticleManager Instance { get { return _instance; } }

    public ParticleSystem hitEffect;
    public ParticleSystem hitMetalEffect;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void HitEffect(Vector3 position, Vector3 normal) {
        hitEffect.transform.position = position;
        hitEffect.transform.forward = normal;
        hitEffect.Emit(1);
    }

    public void HitMetalEffect(Vector3 position, Vector3 normal) {
        hitMetalEffect.transform.position = position;
        hitMetalEffect.transform.forward = normal;
        hitMetalEffect.Emit(1);
    }
}
