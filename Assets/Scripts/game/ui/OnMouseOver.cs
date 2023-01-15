using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Choice choice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus<MouseOverPlayerChoice>.Pub(new MouseOverPlayerChoice() {choice = choice});
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventBus<MouseExitPlayerChoice>.Pub(new MouseExitPlayerChoice() {choice = choice});
    }
}
