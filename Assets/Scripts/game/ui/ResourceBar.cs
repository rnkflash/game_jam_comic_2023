using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    public TMP_Text foodCountTXT;
    public TMP_Text fuelCountTXT;
    public TMP_Text moneyCountTXT;
    public TMP_Text distanceCountTXT;

    void Awake() {
        EventBus<PlayerResourcesChanged>.Sub(OnPlayerResourcesChanged);
    }

    void OnDestroy() {
        EventBus<PlayerResourcesChanged>.Unsub(OnPlayerResourcesChanged);
    }    

    private void OnPlayerResourcesChanged(PlayerResourcesChanged msg) {
        foodCountTXT.text = Player.Instance.food.ToString();
        fuelCountTXT.text = Player.Instance.fuel.ToString();
        moneyCountTXT.text = Player.Instance.money.ToString();
        distanceCountTXT.text = Player.Instance.distance.ToString();
    }
}
