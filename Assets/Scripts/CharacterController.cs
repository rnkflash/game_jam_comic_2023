using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Weapon weapon;
    public AnimationEvents animationEvents;

    void Start()
    {
        animationEvents.onAnimationEvent += OnAnimationEvent;
    }

    void OnDestroy()
    {
        animationEvents.onAnimationEvent -= OnAnimationEvent;
    }

    void Update()
    {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }

    public void LookAt(Vector3 target) {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        weapon.AimAt(target);
    }

    public void MoveTo(Vector3 destination) {
        agent.SetDestination(destination);
    }

    public void FireAt(Vector3 target) {
        weapon.Fire();
    }

    void OnAnimationEvent(string eventName) {
        weapon.Fire();  
    }
}
