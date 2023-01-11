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

    public GameObject frontCard;
    public GameObject backCard;
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

      frontCard.SetActive(false);
      backCard.SetActive(false);

      yield return new WaitForSeconds(0.25f);

      frontCard.SetActive(false);
      backCard.SetActive(true);

      yield return new WaitForSeconds(0.25f);

      var character = Characters.Instance.GetCharacterCard(dialog.character);
      var backImage = backCard.GetComponent<Image>();
      var frontImage = frontCard.GetComponent<Image>();
      Sprite spr;
      if (character != null)
        spr = character.image;
      else
        spr = backImage.sprite;
      frontImage.sprite = spr;

      frontCard.SetActive(true);
      backCard.SetActive(false);

      yield return new WaitForSeconds(0.25f);
      
      dialogUI.SetActive(true);
      text.text = dialog.text;
      rightChoiceText.text = dialog.choiceRight.text;
      leftChoiceText.text = dialog.choiceLeft.text;

      state = DialogueControllerState.Exit;
    }
  }
}