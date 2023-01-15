using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueControllerState
{
  Start,
  ShowCardAnimation,
  Exit
}

namespace game
{
  public class DialogController : MonoBehaviour
  {
    private DialogueControllerState state;

    public Cards cards;
    public GameObject dialogUI;
    public TMP_Text text;
    public TMP_Text rightChoiceText;
    public TMP_Text leftChoiceText;

    public IEnumerator StartNextDialogue()
    {
      state = DialogueControllerState.Start;

      if (Player.Instance.card != null) {
        StartCoroutine(ShowCardAnimation());
      }
      
      while (state != DialogueControllerState.Exit)
      {
        yield return null;
      }
    }

    private IEnumerator ShowCardAnimation() {
      state = DialogueControllerState.ShowCardAnimation;
      var dialog = Player.Instance.GetCurrentDialog();
      var prevDialog = Player.Instance.GetPrevDialog();

      if (prevDialog == null || prevDialog.character != dialog.character) {

        var character = Characters.Instance.GetCharacterCard(dialog.character)?.image ?? Characters.Instance.GetCharacterCard(Character.NO).image;
        cards.FlipFrontCard(character, Player.Instance.dealtCards.Count == 1);

        yield return new WaitForSeconds(0.25f);
      }
      
      dialogUI.SetActive(true);
      text.text = dialog.text;
      rightChoiceText.text = dialog.choiceRight.text;
      leftChoiceText.text = dialog.choiceLeft.text;

      state = DialogueControllerState.Exit;
    }
  }
}