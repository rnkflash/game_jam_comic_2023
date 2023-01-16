using System;
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
    private TextMeshProUGUI distanceBar;

    public TMP_Text debugTriggers;

    void Awake() {
        foodBar = food.GetComponentInChildren<ProgressBarAnimated>();
        fuelBar = fuel.GetComponentInChildren<ProgressBarAnimated>();
        moneyBar = money.GetComponentInChildren<ProgressBarAnimated>();
        distanceBar = distance.GetComponentInChildren<TextMeshProUGUI>();

        foodBar.Init(0, Balance.values.max_food);
        fuelBar.Init(0, Balance.values.max_fuel);
        moneyBar.Init(0, Balance.values.max_money);
        
        EventBus<PlayerResourcesChanged>.Sub(OnPlayerResourcesChanged);
        EventBus<CarMovedDistance>.Sub(OnCarMoved);
    }

    void OnDestroy() {
        EventBus<PlayerResourcesChanged>.Unsub(OnPlayerResourcesChanged);
        EventBus<CarMovedDistance>.Unsub(OnCarMoved);
    }    

    private void OnPlayerResourcesChanged(PlayerResourcesChanged msg) {
        foodBar.SetValue(Player.Instance.GetResource(Resource.food));
        fuelBar.SetValue(Player.Instance.GetResource(Resource.fuel));
        moneyBar.SetValue(Player.Instance.GetResource(Resource.money));
    }

    private void OnCarMoved(CarMovedDistance message)
    {
        distanceBar.text = $"{(Mathf.Clamp(Balance.values.max_distance - message.distance * 100.0f, 0, 999999)).ToString("F0")} м";
    }

}
