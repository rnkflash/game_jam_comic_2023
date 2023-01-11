using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SituationConclusionControllerState { 
    Start,
    Exit
}

public class SituationConclusionController : MonoBehaviour {

    private SituationConclusionControllerState state;
    public IEnumerator StartConclusion()
    {
        state = SituationConclusionControllerState.Start;

        var choice = Player.Instance.GetCurrentChoice();

        Player.Instance.dialog = -1;
        choice.actions.ForEach(action => DoAction(action));

        EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());

        yield return new WaitForSeconds(1.0f);

        state = SituationConclusionControllerState.Exit;
        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }

    private void DoAction(ReignsTypeCard.Action action) {
        switch(action.type) 
        {
        case ReignsTypeCard.Action.ActionType.dialog:
            Player.Instance.dialog = action.value;
            break;
        case ReignsTypeCard.Action.ActionType.food:
            Player.Instance.food += action.value;
            break;
        case ReignsTypeCard.Action.ActionType.fuel:
            Player.Instance.fuel += action.value;
            break;
        case ReignsTypeCard.Action.ActionType.money:
            Player.Instance.money += action.value;
            break;
        case ReignsTypeCard.Action.ActionType.distance:
            Player.Instance.distance += action.value;
            break;
        default:
            break;
        }
    }
}