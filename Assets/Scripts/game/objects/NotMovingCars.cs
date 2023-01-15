using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMovingCars : MonoBehaviour
{
    public float distanceBetweenCarsMin = 2.5f;
    public float distanceBetweenCarsMax = 5.0f;
    public List<GameObject> cars = new List<GameObject>();

    private List<GameObject> onScreenCars = new List<GameObject>();

    void Start()
    {
        FillScreenWithCars();
    }

    private void FillScreenWithCars() {
        var removeCarX = Camera.main.ScreenToWorldPoint(new Vector3(-200, 0, Camera.main.transform.position.z)).x;
        var randomPrefab = cars[Random.Range(0,cars.Count-1)];
        Instantiate(randomPrefab);
    }

    void Update()
    {
        var removeCarX = Camera.main.ScreenToWorldPoint(new Vector3(-200, 0, Camera.main.transform.position.z)).x;

    }
}
