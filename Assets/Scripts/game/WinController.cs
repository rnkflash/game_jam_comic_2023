using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum WinControllerState { 
    Start,
    PressAnyKey,
    Exit
}

public class WinController : MonoBehaviour
{
    private WinControllerState state;
    public GameObject ui;
    public Image bg;
    public GameObject popup;
    public TMP_Text pressAnyKeyText;

    public IEnumerator Show()
    { 
        state = WinControllerState.Start;

        ui.SetActive(true);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("victory"));
        
        bg.DOFade(0.9f, 0.5f);

        var origPos = popup.transform.position;
        popup.transform.position = new Vector3(origPos.x, 1000, origPos.z);
        popup.transform.DOMove(origPos, 0.5f).SetEase(Ease.OutCubic).OnComplete(()=> {
            pressAnyKeyText.DOFade(1, .25f);
            state = WinControllerState.PressAnyKey;
        });

        while (state != WinControllerState.Exit)
        {
            yield return null;
        }
    }

    void Update() {
        if (state == WinControllerState.PressAnyKey && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
            state = WinControllerState.Exit;
    }
}
