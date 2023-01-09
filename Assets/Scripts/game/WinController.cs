using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinControllerState { 
    Start,
    PressAnyKey,
    Exit
}

public class WinController : MonoBehaviour
{
    private WinControllerState state;
    public GameObject ui;

    public IEnumerator Show()
    { 
        state = WinControllerState.Start;

        ui.SetActive(true);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("victory"));
        StartCoroutine(Wait1Sec());

        while (state != WinControllerState.Exit)
        {
            yield return null;
        }
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(1);
        state = WinControllerState.PressAnyKey;
    }

    void Update() {
        if (state == WinControllerState.PressAnyKey && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
            state = WinControllerState.Exit;
    }
}
