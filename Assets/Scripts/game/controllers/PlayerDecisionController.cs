using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerDecisionControllerState { 
    Start,
    Exit
}

public class PlayerDecisionController : MonoBehaviour {

    private PlayerDecisionControllerState state;

    public GameObject dialogUI;
    public Cards cards;

    void Awake() {
        EventBus<PlayerDecisionMade>.Sub(OnPlayerDecision);
    }

    void OnDestroy() {
        EventBus<PlayerDecisionMade>.Unsub(OnPlayerDecision);
    }

    public IEnumerator StartMakingDecision()
    {
        state = PlayerDecisionControllerState.Start;
        
        while (state != PlayerDecisionControllerState.Exit)
        {
            yield return null;
        }
    }

    private void OnPlayerDecision(PlayerDecisionMade msg) {
        Player.Instance.choice = msg.decision;

        cards.StopLeaning();

        state = PlayerDecisionControllerState.Exit;
        dialogUI.SetActive(false);
    }
}