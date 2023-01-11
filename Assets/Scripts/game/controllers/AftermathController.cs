using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AftermathController : MonoBehaviour {
    public enum State { 
        Start,
        PressAnyKey,
        Exit
    }
    private State state;

    public GameObject aftermathUI;
    public TMP_Text text;
    public TMP_Text pressAnyKeyText;

    public IEnumerator ShowAftermath()
    {
        state = State.Start;
        aftermathUI.SetActive(true);

        ReignsTypeCard.Choice choice;
        if (Player.Instance.choice == Choice.Right)
            choice = Player.Instance.card.dialogs[Player.Instance.dialog].choiceRight;
        else
            choice = Player.Instance.card.dialogs[Player.Instance.dialog].choiceLeft;

        text.text = choice.aftermath;

        pressAnyKeyText.DOFade(1, .25f).OnComplete(()=> {
            state = State.PressAnyKey;
        });
        
        while (state != State.Exit)
        {
            yield return null;
        }
    }

    void Update() {
        if (state == State.PressAnyKey && (Input.GetMouseButtonDown(0) || Input.anyKeyDown)) {
            aftermathUI.SetActive(false);
            state = State.Exit;
        }
    }
}