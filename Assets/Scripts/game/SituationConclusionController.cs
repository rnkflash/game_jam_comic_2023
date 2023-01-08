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
            Player.Instance.fuel += SituationStartController.card.YesFuel;
            Player.Instance.money += SituationStartController.card.YesMoney;
            Player.Instance.food += SituationStartController.card.YesFood;
            Player.Instance.distance += SituationStartController.card.YesDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        if (PlayerDecisionController.lastDecision == PlayerDecision.No) {
            Player.Instance.fuel += SituationStartController.card.NoFuel;
            Player.Instance.money += SituationStartController.card.NoMoney;
            Player.Instance.food += SituationStartController.card.NoFood;
            Player.Instance.distance += SituationStartController.card.NoDistance;

            EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
        }

        state = SituationConclusionControllerState.Exit;

        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }
}