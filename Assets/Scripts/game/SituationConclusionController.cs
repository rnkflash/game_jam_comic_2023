using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SituationConclusionControllerState { 
    Show,
    Exit
}

public class SituationConclusionController : MonoBehaviour {

    private SituationConclusionControllerState state;

    public GameObject ui;

    public IEnumerator StartConclusion(int dialogNumber)
    {
        state = SituationConclusionControllerState.Show;

        ui.SetActive(true);

        if (PlayerDecisionController.lastDecision == PlayerDecision.Yes) {
            Player.Instance.fuel += SituationStartController.card.DialogSegments[dialogNumber].YesFuel;
            Player.Instance.money += SituationStartController.card.DialogSegments[dialogNumber].YesMoney;
            Player.Instance.food += SituationStartController.card.DialogSegments[dialogNumber].YesFood;
            Player.Instance.distance += SituationStartController.card.DialogSegments[dialogNumber].YesDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        if (PlayerDecisionController.lastDecision == PlayerDecision.No) {
            Player.Instance.fuel += SituationStartController.card.DialogSegments[dialogNumber].NoFuel;
            Player.Instance.money += SituationStartController.card.DialogSegments[dialogNumber].NoMoney;
            Player.Instance.food += SituationStartController.card.DialogSegments[dialogNumber].NoFood;
            Player.Instance.distance += SituationStartController.card.DialogSegments[dialogNumber].NoDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        state = SituationConclusionControllerState.Exit;

        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }
}