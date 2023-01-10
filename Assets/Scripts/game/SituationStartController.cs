using System;
using System.Collections;
using System.Collections.Generic;
using StaticData;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SituationStartControllerState
{
  Start,
  Select,
  Show,
  Exit
}

public class SituationStartController : MonoBehaviour
{
  private SituationStartControllerState state;
  public static CardStaticData card;
  public CardsArray CardsArray = new CardsArray();

  private List<int> notUsedNumbers = new List<int>();
  private int checkPoint = 7000;

  public GameObject ui;
  public TMP_Text questionText;

  private void Awake()
  {
    CardsArray.CreateCardArrays();
    CreateNewNotUsedNumberList(CardsArray.CardsLv1.Length);
  }

  public IEnumerator StartNextSituation()
  {
    state = SituationStartControllerState.Start;

    ui.SetActive(true);

    CheckPointCheck();

    card = GetEventCard();

    state = SituationStartControllerState.Exit;

    while (state != SituationStartControllerState.Exit)
    {
      yield return null;
    }
  }

  private CardStaticData GetEventCard()
  {
    if (Player.Instance.distance < 7000) return GetRandomCardFrom(CardsArray.CardsLv1);
    else if (Player.Instance.distance < 14000) return GetRandomCardFrom(CardsArray.CardsLv2);
    else return GetRandomCardFrom(CardsArray.CardsLv3);
  }

  private CardStaticData GetRandomCardFrom(CardStaticData[] cardsArrayCards)
  {
    if (notUsedNumbers.Count == 0) Debug.Log("Oooops! Not used numbers ends!");
    int rnd = Random.Range(0, notUsedNumbers.Count - 1);
    int randomNumber = notUsedNumbers[rnd];
    notUsedNumbers.RemoveAt(rnd);
    return cardsArrayCards[randomNumber];
  }

  private void CreateNewNotUsedNumberList(int cardsLength)
  {
    notUsedNumbers.Clear();
    for (int i = 0; i < cardsLength; i++)
      notUsedNumbers.Add(i);
  }

  private void CheckPointCheck()
  {
    if (Player.Instance.distance >= checkPoint)
    {
      CreateNewNotUsedNumberList(CardsArray.CardsLv2.Length);
      checkPoint += 7000;
    }
  }
}