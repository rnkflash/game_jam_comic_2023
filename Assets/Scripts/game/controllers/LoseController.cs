using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LoseControllerState { 
    Start,
    PressAnyKey,
    Exit
}

public class LoseController : MonoBehaviour
{
    private LoseControllerState state;
    public GameObject ui;
    public Image bg;
    public GameObject popup;
    public TMP_Text pressAnyKeyText;
    public GameObject text;

    public IEnumerator Show()
    { 
        state = LoseControllerState.Start;

        ui.SetActive(true);
        text.SetActive(true);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("paper_check"));

        bg.DOFade(0.9f, 0.5f);
        
        var origPos = popup.transform.position;
        popup.transform.position = new Vector3(origPos.x, 1000, origPos.z);
        popup.transform.DOMove(origPos, 0.5f).SetEase(Ease.OutCubic).OnComplete(()=> {
            //text.SetActive(true);
            pressAnyKeyText.DOFade(1, .25f);
            state = LoseControllerState.PressAnyKey;
        });

        while (state != LoseControllerState.Exit)
        {
            yield return null;
        }
    }

    void Update() {
        
        if (state == LoseControllerState.PressAnyKey && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
            state = LoseControllerState.Exit;
    }
}
