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

    public GameObject ui;
    public GameObject dialogUI;

    void Awake() {
        EventBus<PlayerDecisionMade>.Sub(OnPlayerDecision);
    }

    void OnDestroy() {
        EventBus<PlayerDecisionMade>.Unsub(OnPlayerDecision);
    }

    public IEnumerator StartMakingDecision()
    {
        state = PlayerDecisionControllerState.Start;
        
        ui.SetActive(true);

        while (state != PlayerDecisionControllerState.Exit)
        {
            yield return null;
        }
    }

    private void OnPlayerDecision(PlayerDecisionMade msg) {
        Player.Instance.choice = msg.decision;

        state = PlayerDecisionControllerState.Exit;
        ui.SetActive(false);
        dialogUI.SetActive(false);
    }
}