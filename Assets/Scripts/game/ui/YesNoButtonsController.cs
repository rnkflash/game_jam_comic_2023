using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoButtonsController : MonoBehaviour {
    public void ButtonClicked(int decisionInt) {
        
        Choice decisionEnum = Choice.Right;
        if (decisionInt == 1)
            decisionEnum = Choice.Left;

        EventBus<PlayerDecisionMade>.Pub(new PlayerDecisionMade() { decision = decisionEnum } );
    }
}