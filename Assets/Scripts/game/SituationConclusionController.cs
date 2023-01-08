using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SituationConclusionControllerState { 
    Show,
    Exit
}

public class SituationConclusionController : MonoBehaviour {

    private SituationConclusionControllerState state;

    public IEnumerator StartConclusion()
    {
        state = SituationConclusionControllerState.Show;

        while (state != SituationConclusionControllerState.Exit)
        {
            yield return null;
        }
    }
}