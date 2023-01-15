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
  public Cards cards;
  

  public IEnumerator StartNextSituation()
  {
    var availableCards = GetAvailableCards();

    List<string> newCards = new List<string>();
    List<string> removedCards = new List<string>();
    List<string> remainedCards = new List<string>();
    availableCards.ForEach(c=>{
      if (!Player.Instance.dealtCards.Contains(c.id)) {
        newCards.Add(c.id);
      }
    });
    Player.Instance.dealtCards.ForEach(cid=>{
      if (!availableCards.Any(c=>c.id == cid)) {
        removedCards.Add(cid);
      } else {
        remainedCards.Add(cid);
      }
    });
    Player.Instance.dealtCards = remainedCards.Concat(newCards).ToList();
    
    if (newCards.Count > 0) {
      cards.AddCards(newCards.Count);
      while (cards.state != Cards.State.hidden) {
        yield return null;
      }
        
    }

    ReignsTypeCard selectedCard = null;
    if (availableCards.Count() > 0) {
      var GroupByPriority = availableCards.GroupBy(s => s.conditions.priority)
                            .OrderByDescending(c => c.Key)
                            .Select(std => new {
                                Key = std.Key,
                                Cards = std.ToList()
                            });

      foreach (var group in GroupByPriority)
      {
        if (group.Cards.Count > 0) {
          selectedCard = group.Cards.ElementAt(Random.Range(0, group.Cards.Count - 1));
          break;
        }
      }
    }

    if (selectedCard != null) {
      Player.Instance.usedCards.Add(selectedCard.id);
      Player.Instance.prevDialog = -1;
      Player.Instance.dialog = selectedCard.dialogs[0].id;
    }

    Player.Instance.card = selectedCard;

    state = SituationStartControllerState.Exit;
    while (state != SituationStartControllerState.Exit)
    {
      yield return null;
    }
  }

  private List<ReignsTypeCard> GetAvailableCards() {
    var availableCards = CardsArray.Instance.newCards.Where(card =>
      !Player.Instance.usedCards.Any(id => id == card.id) 
      && card.enabled
      && PlayerHasMetConditions(card.conditions)
    ).ToList();

    return availableCards.OrderByDescending(o=>o.conditions.priority).ToList()
        .OrderBy(o=>o.conditions.resources.Find(r=>r.resource == Resource.distance)?.min ?? 0).ToList();
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