using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Car : MonoBehaviour
{
    public enum State {
        Stop,
        Moving
    }

    public State state;

    void Start() {
        state = State.Stop;
    }

    public void Move(float distance)
    {
        state = State.Moving;
        transform.DOMove(
            new Vector3(transform.position.x + distance, transform.position.y, transform.position.z), 2
         ).SetEase(Ease.InOutCubic).OnComplete(()=>{state = State.Stop;});
    }
}