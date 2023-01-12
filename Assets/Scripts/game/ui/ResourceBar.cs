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
    }

    void OnDestroy() {
        EventBus<PlayerResourcesChanged>.Unsub(OnPlayerResourcesChanged);
    }    

    private void OnPlayerResourcesChanged(PlayerResourcesChanged msg) {
        foodBar.SetValue(Player.Instance.GetResource(Resource.food));
        fuelBar.SetValue(Player.Instance.GetResource(Resource.fuel));
        moneyBar.SetValue(Player.Instance.GetResource(Resource.money));
        
        var distanceLeft = Mathf.Clamp(Balance.values.max_distance - Player.Instance.GetResource(Resource.distance), 0, Balance.values.max_distance);
        distanceBar.text = "Осталось проехать: "+$"{distanceLeft}";

        debugTriggers.text = "";
        Player.Instance.triggers.ForEach(trigger=>debugTriggers.text += Enum.GetName(typeof(Trigger), trigger));
    }
}
