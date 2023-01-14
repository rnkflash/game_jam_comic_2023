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

        Player.Instance.prevDialog = Player.Instance.dialog;
        Player.Instance.dialog = -1;
        choice.actions.ForEach(action => DoAction(action.data));

        if (Player.Instance.dialog == -1)
            Balance.values.each_card_resources.ForEach(r => {
                Player.Instance.AddResource(r.type, r.amount);
            });

        EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());

        yield return new WaitForSeconds(1.0f);

        state = SituationConclusionControllerState.Exit;
        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }

    private void DoAction(ReignsTypeCard.Action action) {
        if (action.GetType() == typeof(ReignsTypeCard.ResourceAction)) {
            var resourceAction = action as ReignsTypeCard.ResourceAction;
            Player.Instance.AddResource(resourceAction.resource, resourceAction.amount);
        } else if (action.GetType() == typeof(ReignsTypeCard.DialogAction)) {
            var dialogAction = action as ReignsTypeCard.DialogAction;
            Player.Instance.dialog = dialogAction.id;
        } else if (action.GetType() == typeof(ReignsTypeCard.TriggerAction)) {
            var triggerAction = action as ReignsTypeCard.TriggerAction;
            if (triggerAction.have)
                Player.Instance.triggers.Add(triggerAction.trigger);
            else
                Player.Instance.triggers.Remove(triggerAction.trigger);
        }
    }
}