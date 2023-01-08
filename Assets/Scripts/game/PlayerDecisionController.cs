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

    public IEnumerator StartMakingDecision()
    {
        state = PlayerDecisionControllerState.Start;

        while (state != PlayerDecisionControllerState.Exit)
        {
            yield return null;
        }
    }
}