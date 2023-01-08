using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoButtonsController : MonoBehaviour {
    public void ButtonClicked(int decisionInt) {
        
        PlayerDecision decisionEnum = PlayerDecision.Yes;
        if (decisionInt == 1)
            decisionEnum = PlayerDecision.No;

        EventBus<PlayerDecisionMade>.Pub(new PlayerDecisionMade() { decision = decisionEnum } );
    }
}