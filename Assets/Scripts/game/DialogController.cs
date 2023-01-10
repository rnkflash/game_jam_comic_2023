using System.Collections;
using TMPro;
using UnityEngine;

public enum DialogueControllerState
{
  Start,
  Select,
  Show,
  Exit
}

namespace game
{
  public class DialogController : MonoBehaviour
  {
    private DialogueControllerState state;

    public GameObject UI;
    public TMP_Text questionText;

    public IEnumerator StartNextDialogue(int dialogNumber)
    {
      state = DialogueControllerState.Start;
      
      UI.SetActive(true);

      questionText.text = SituationStartController.card.DialogSegments[dialogNumber].Question;

      state = DialogueControllerState.Exit;

      while (state != DialogueControllerState.Exit)
      {
        yield return null;
      }
    }
  }
}