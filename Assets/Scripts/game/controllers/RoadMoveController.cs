using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoadMoveController : MonoBehaviour
{
    public enum State { 
        Start,
        Moving,
        Exit
    }
    private State state;

    public GameObject carObject;
    private Car car;

    void Start() {
        car = carObject.GetComponent<Car>();
    }

    public IEnumerator Move()
    { 
        state = State.Start;

        while (state != State.Exit)
        {
            yield return null;
        }
    }

    private void RandomMove() {
        car.Move(Random.Range(1.0f,10.0f));
        state = State.Moving;
    }

    void Update() {
        if (state == State.Start)
            RandomMove();

        if (state == State.Moving && car.state == Car.State.Stop) {
            state = State.Exit;
        }
    }
}
