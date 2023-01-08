using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDecisionControllerState { 
    Start,
    Select,
    Show,
    Exit
}

public class PlayerDecisionController : MonoBehaviour {

    private PlayerDecisionControllerState state;

    public GameObject ui;

    public static PlayerDecision lastDecision;

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

        lastDecision = msg.decision;
        ui.SetActive(false);
        state = PlayerDecisionControllerState.Exit;
    }
}