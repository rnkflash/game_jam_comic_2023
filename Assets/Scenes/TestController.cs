using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject cardsObject;
    private Cards cards;

    void Start()
    {
        cards = cardsObject.GetComponent<Cards>();
        StartCoroutine(DelayedStart());

        EventBus<PlayerDecisionMade>.Sub(OnPlayerDecisionMade);
    }

    private void OnPlayerDecisionMade(PlayerDecisionMade message)
    {
        if (message.decision == Choice.Left)
            cards.FallDownLeft();
        else
            cards.FallDownRight();
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(2.0f);
        cards.AddCards(10);
        yield return new WaitForSeconds(2.0f);
        cards.FlipFrontCard(Characters.Instance.GetCharacterCard(Character.Журналистлиберал).image);
    }

    void Update()
    {
        
    }
    
}
