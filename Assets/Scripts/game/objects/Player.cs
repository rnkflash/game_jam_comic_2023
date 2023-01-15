using System;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player Instance;
    public Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
    public List<Trigger> triggers = new List<Trigger>();

    public ReignsTypeCard card = null;
    public int dialog = -1;
    public int prevDialog = -1;
    public List<string> usedCards;
    public Choice choice;

    public float movedDistance = 0;

    public List<string> dealtCards = new List<string>();

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

    public ReignsTypeCard.Dialog GetPrevDialog() {
        if (prevDialog == -1)
            return null;
        return card.dialogs[prevDialog];
    }    

    public ReignsTypeCard.Choice GetCurrentChoice() {
        ReignsTypeCard.Choice choiceData;
        if (choice == Choice.Right)
            choiceData = GetCurrentDialog().choiceRight;
        else
            choiceData = GetCurrentDialog().choiceLeft;
        return choiceData;
    }

    public int GetResource(Resource resource) {
        int value;
        if (resources.TryGetValue(resource, out value))
            return value;
        else
            return 0;
    }

    public void AddResource(Resource resource, int amount) {
        if (!resources.ContainsKey(resource))
            resources[resource] = 0;
        resources[resource] += amount;
    }

    public void SetResource(Resource resource, int amount) {
        if (!resources.ContainsKey(resource))
            resources[resource] = 0;
        resources[resource] = amount;
    }

}