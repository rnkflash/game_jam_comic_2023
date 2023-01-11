using System;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player Instance;

    public float distance = 100;
    public int money = 100;
    public int food = 99;
    public int fuel = 100;

    public ReignsTypeCard card = null;
    public int dialog = -1;
    public List<string> usedCards;
    public Choice choice;

    void Awake() {
        Instance = this;
    }

    void OnDestroy() {
        Instance = null;
    }

    public ReignsTypeCard.Dialog GetCurrentDialog() {
        if (dialog == -1)
            return null;
        return card.dialogs[dialog];
    }

    public ReignsTypeCard.Choice GetCurrentChoice() {
        ReignsTypeCard.Choice choiceData;
        if (choice == Choice.Right)
            choiceData = GetCurrentDialog().choiceRight;
        else
            choiceData = GetCurrentDialog().choiceLeft;
        return choiceData;
    }

}