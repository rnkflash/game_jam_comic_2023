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

    private float car_orig_x;

    void Start() {
        car = carObject.GetComponent<Car>();
        car_orig_x = car.transform.position.x;
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

        var distanceToMove = Player.Instance.GetResource(Resource.distance) - Player.Instance.movedDistance;
        Player.Instance.movedDistance = Player.Instance.GetResource(Resource.distance);
        car.Move(distanceToMove / 100.0f);
        state = State.Moving;
    }

    private float sendDistance = 0.0f;

    void Update() {
        if (state == State.Start)
            RandomMove();


        if (state == State.Moving) {
            sendDistance +=Time.deltaTime;
            if (sendDistance>0.15f) {
                EventBus<CarMovedDistance>.Pub(new CarMovedDistance() {distance = car.transform.position.x - car_orig_x});
                sendDistance = 0.0f;
            }
        }

        if (state == State.Moving && car.state == Car.State.Stop) {
            state = State.Exit;
            EventBus<CarMovedDistance>.Pub(new CarMovedDistance() {distance = car.transform.position.x - car_orig_x});
        }
    }
}
