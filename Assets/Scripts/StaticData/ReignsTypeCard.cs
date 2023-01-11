using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ReignsTypeCard", menuName = "Static Data/ReignsTypeCard")]
public class ReignsTypeCard : ScriptableObject
{
    public string id;
    public List<Condition> conditions;
    public List<Dialog> dialogs;

    [Serializable]
    public class Condition{
        public int distance;
    }

    [Serializable]
    public class Dialog{
        public int id;
        public Character character;
        public string text;
        public Choice choiceLeft;
        public Choice choiceRight;
    }

    [Serializable]
    public class Choice {
        public string text;
        public string aftermath;
        public List<Action> actions;
    }

    [Serializable]
    public class Action {
        public ActionType type;
        public int value;

        public enum ActionType
        {
            food,
            fuel,
            money,
            distance,
            dialog
        }
    }
}