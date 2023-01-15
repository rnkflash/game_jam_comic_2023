using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesButtons : MonoBehaviour
{
    public Button right_choice;
    public Button left_choice;

    void Start()
    {
        right_choice.onClick.AddListener(() => ButtonClicked(Choice.Right));
        left_choice.onClick.AddListener(() => ButtonClicked(Choice.Left));
    }

    void ButtonClicked(Choice choice)
    {
        EventBus<PlayerDecisionMade>.Pub(new PlayerDecisionMade() {decision = choice});
    }

}
