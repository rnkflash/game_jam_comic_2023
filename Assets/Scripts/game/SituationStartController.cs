using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SituationStartControllerState { 
    Start,
    Select,
    Show,
    Exit
}

public class SituationStartController : MonoBehaviour {

    private SituationStartControllerState state;

    public IEnumerator StartNextSituation()
    {
        state = SituationStartControllerState.Start;

        while (state != SituationStartControllerState.Exit)
        {
            yield return null;
        }
    }
}