using System.Collections;
using System.Collections.Generic;
using StaticData;
using TMPro;
using UnityEngine;

public enum SituationStartControllerState { 
    Start,
    Select,
    Show,
    Exit
}

public class SituationStartController : MonoBehaviour {

    private SituationStartControllerState state;
    public static CardStaticData card;
    
    public GameObject ui;
    public TMP_Text questionText;

    public IEnumerator StartNextSituation()
    {
        state = SituationStartControllerState.Start;

        ui.SetActive(true);

        card = new CardStaticData() {
            Question = "just a debug situation",
            YesFuel = 10,
            YesFood = 10,
            YesDistance = 10,
            YesMoney = 10,

            NoFood = -10,
            NoDistance = 0,
            NoFuel = -10,
            NoMoney = -10
        };

        questionText.text = card.Question;
        state = SituationStartControllerState.Exit;

        while (state != SituationStartControllerState.Exit)
        {
            yield return null;
        }
    }
}