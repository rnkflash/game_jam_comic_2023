using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject answerUI;
    public TMP_Text answerText;
    public GameObject situationUI;

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
    void Update() {
        if (state == PlayerDecisionControllerState.Show && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            state = PlayerDecisionControllerState.Exit;
            answerUI.SetActive(false);
        }
    }

    private void OnPlayerDecision(PlayerDecisionMade msg) {

        state = PlayerDecisionControllerState.Show;
        lastDecision = msg.decision;
        ui.SetActive(false);
        situationUI.SetActive(false);
        answerUI.SetActive(true);
        if (lastDecision == PlayerDecision.Yes) answerText.text = SituationStartController.card.YesAnswer;
        else answerText.text = SituationStartController.card.NoAnswer;
    }
}