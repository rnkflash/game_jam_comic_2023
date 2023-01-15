using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotMovingCars : MonoBehaviour
{
    public float distanceBetweenCarsMin = 2.5f;
    public float distanceBetweenCarsMax = 5.0f;
    public List<GameObject> cars = new List<GameObject>();
    private List<GameObject> onScreenCars = new List<GameObject>();
    private float totalCarsWidth = -5.0f;

    void Start()
    {
        FillScreenWithCars();
    }

    private void FillScreenWithCars() {
        var cameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
        var c = 0;
        do {
            var randomPrefab = cars[Random.Range(0,cars.Count-1)];
            var nextPosition = totalCarsWidth + 5.0f;
            var car = Instantiate(randomPrefab, new Vector3(nextPosition, 0, 0), Quaternion.identity);
            car.transform.SetParent(transform, false);
            var temp = car.transform.position;
            temp.y = car.GetComponent<SpriteRenderer>().bounds.size.y;
            car.transform.position = temp;
            totalCarsWidth = nextPosition;
            onScreenCars.Add(car);
            c++;
        } while(totalCarsWidth < cameraBounds.x*2 && c<10 );
    }

    void Update()
    {
        var removeCarX = Camera.main.ScreenToWorldPoint(new Vector3(-200, 0, Camera.main.transform.position.z)).x;

    }
}
