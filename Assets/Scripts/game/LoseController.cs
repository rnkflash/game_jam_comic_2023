using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoseControllerState { 
    Start,
    PressAnyKey,
    Exit
}

public class LoseController : MonoBehaviour
{
    private LoseControllerState state;
    public GameObject ui;

    public IEnumerator Show()
    { 
        state = LoseControllerState.Start;

        ui.SetActive(true);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("paper_check"));
        StartCoroutine(Wait1Sec());

        while (state != LoseControllerState.Exit)
        {
            yield return null;
        }
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(1);
        state = LoseControllerState.PressAnyKey;
    }


    void Update() {
        
        if (state == LoseControllerState.PressAnyKey && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
            state = LoseControllerState.Exit;
    }
}
