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
            Question = "just a debug situation " + Random.Range(0,999),
            YesFuel = Random.Range(0,10),
            YesFood = Random.Range(0,10),
            YesDistance = Random.Range(0,10.0f),
            YesMoney = Random.Range(0,10),

            NoFood = Random.Range(0,-10),
            NoDistance = 0,
            NoFuel = Random.Range(0,-10),
            NoMoney = Random.Range(0,-10),
        };

        questionText.text = card.Question;
        state = SituationStartControllerState.Exit;

        while (state != SituationStartControllerState.Exit)
        {
            yield return null;
        }
    }
}