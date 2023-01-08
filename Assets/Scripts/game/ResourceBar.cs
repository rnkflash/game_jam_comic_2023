using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    public TMP_Text foodCountTXT;
    public TMP_Text fuelCountTXT;
    public TMP_Text moneyCountTXT;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.onResourcesChanged += OnResourcesChanged;
        OnResourcesChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnResourcesChanged() {
        foodCountTXT.text = Player.Instance.food.ToString();
        fuelCountTXT.text = Player.Instance.fuel.ToString();
        moneyCountTXT.text = Player.Instance.money.ToString();
    }
}
