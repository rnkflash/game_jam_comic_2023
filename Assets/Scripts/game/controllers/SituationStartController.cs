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
      !Player.Instance.usedCards.Any(id => id == card.id) 
      && card.enabled
      && PlayerHasMetConditions(card.conditions)
    ).ToList();

    ReignsTypeCard selectedCard;
    var countCards = availableCards.Count();
    if (countCards == 0)
      selectedCard = null;
    else {
      var sortedCards = availableCards.OrderByDescending(o=>o.conditions.priority).ToList()
        .OrderBy(o=>o.conditions.resources.Find(r=>r.resource == Resource.distance)?.min ?? 0);
      selectedCard = sortedCards.First();
    }

    if (selectedCard != null) {
      Player.Instance.usedCards.Add(selectedCard.id);
      Player.Instance.prevDialog = -1;
      Player.Instance.dialog = selectedCard.dialogs[0].id;
    }

    return selectedCard;
  }

    private bool PlayerHasMetConditions(ReignsTypeCard.Condition conditions)
    {
      var resourceCondition = conditions.resources.All(condition => {
          var value = Player.Instance.GetResource(condition.resource);
          return value >= condition.min && value <= condition.max;
      });

      var triggerConditions = conditions.triggers.All(trigger => {
        var have = Player.Instance.triggers.Contains(trigger.trigger);
        return trigger.have == have;
      });

      return resourceCondition && triggerConditions;
    }
}