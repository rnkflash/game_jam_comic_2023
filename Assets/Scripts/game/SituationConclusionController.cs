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

    public IEnumerator StartConclusion()
    {
        state = SituationConclusionControllerState.Show;

        ui.SetActive(true);

        if (PlayerDecisionController.lastDecision == PlayerDecision.Yes) {
            Player.Instance.fuel += SituationStartController.card.DialogSegments[0].YesFuel;
            Player.Instance.money += SituationStartController.card.DialogSegments[0].YesMoney;
            Player.Instance.food += SituationStartController.card.DialogSegments[0].YesFood;
            Player.Instance.distance += SituationStartController.card.DialogSegments[0].YesDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        if (PlayerDecisionController.lastDecision == PlayerDecision.No) {
            Player.Instance.fuel += SituationStartController.card.DialogSegments[0].NoFuel;
            Player.Instance.money += SituationStartController.card.DialogSegments[0].NoMoney;
            Player.Instance.food += SituationStartController.card.DialogSegments[0].NoFood;
            Player.Instance.distance += SituationStartController.card.DialogSegments[0].NoDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        state = SituationConclusionControllerState.Exit;

        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }
}