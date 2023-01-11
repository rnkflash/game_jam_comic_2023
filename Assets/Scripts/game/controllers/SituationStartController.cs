using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StaticData;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SituationStartControllerState
{
  Start,
  Exit
}

public class SituationStartController : MonoBehaviour
{
  private SituationStartControllerState state;

  public IEnumerator StartNextSituation()
  {
    Player.Instance.card = GetAvailableCardOrNull();

    state = SituationStartControllerState.Exit;
    while (state != SituationStartControllerState.Exit)
    {
      yield return null;
    }
  }

  private ReignsTypeCard GetAvailableCardOrNull() {
    var availableCards = CardsArray.Instance.newCards.Where(card =>
      !Player.Instance.usedCards.Any(id => id == card.id) && card.conditions.All(condition =>
        condition.distance <= Player.Instance.distance
      )
    ).ToList();
    
    ReignsTypeCard selectedCard;
    var countCards = availableCards.Count();
    if (countCards == 0)
      selectedCard = null;
    else
      selectedCard = availableCards.ElementAt(Random.Range(0,countCards-1));

    if (selectedCard != null) {
      Player.Instance.usedCards.Add(selectedCard.id);
      Player.Instance.prevDialog = -1;
      Player.Instance.dialog = selectedCard.dialogs[0].id;
    }

    return selectedCard;
  }
}