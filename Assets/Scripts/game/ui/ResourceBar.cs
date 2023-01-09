using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    public GameObject food;
    public GameObject fuel;
    public GameObject money;
    public GameObject distance;

    private ProgressBarAnimated foodBar;
    private ProgressBarAnimated fuelBar;
    private ProgressBarAnimated moneyBar;
    private ProgressBarAnimated distanceBar;

    void Awake() {
        foodBar = food.GetComponentInChildren<ProgressBarAnimated>();
        fuelBar = fuel.GetComponentInChildren<ProgressBarAnimated>();
        moneyBar = money.GetComponentInChildren<ProgressBarAnimated>();
        distanceBar = distance.GetComponentInChildren<ProgressBarAnimated>();

        foodBar.Init(0, 100);
        fuelBar.Init(0, 100);
        moneyBar.Init(0, 100);
        distanceBar.Init(0, 100);

        EventBus<PlayerResourcesChanged>.Sub(OnPlayerResourcesChanged);
    }

    void OnDestroy() {
        EventBus<PlayerResourcesChanged>.Unsub(OnPlayerResourcesChanged);
    }    

    private void OnPlayerResourcesChanged(PlayerResourcesChanged msg) {
        foodBar.SetValue(Player.Instance.food);
        fuelBar.SetValue(Player.Instance.fuel);
        moneyBar.SetValue(Player.Instance.money);
        distanceBar.SetValue(Mathf.FloorToInt(Player.Instance.distance));
    }
}
