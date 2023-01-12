using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ReignsTypeCard", menuName = "Static Data/ReignsTypeCard")]
public class ReignsTypeCard : ScriptableObject
{
    public string id;
    public bool enabled = true;
    public Condition conditions;
    public List<Dialog> dialogs;

    [Serializable]
    public class Condition{
        public int priority = 0;
        public List<TriggerCondition> triggers;
        public List<ResourceCondition> resources;
    }

    [Serializable]
    public class TriggerCondition {
        public Trigger trigger;
        public bool have = true;
    }

    [Serializable]
    public class ResourceCondition {
        public Resource resource;
        public int min = 0;
        public int max = 100000;
    }

    [Serializable]
    public class Dialog{
        public int id;
        public Character character;
        [TextArea(3, 100)]
        public string text;
        public Choice choiceLeft;
        public Choice choiceRight;
    }

    [Serializable]
    public class Choice {
        public string text;
        [TextArea(3, 100)]
        public string aftermath;
        public List<ActionObject> actions;
    }

    [Serializable]
    public class ActionObject {
        public ActionType type;
        [SerializeReference] public Action data;

    }

    [Serializable]
    public class Action {
        [HideInInspector] public ActionType type = ActionType.resource;
    }

    [Serializable]
    public enum ActionType {
        resource,
        dialog,
        trigger
    }

    [Serializable]
    public class ResourceAction: Action {
        public Resource resource;
        public int amount;

        public ResourceAction() {type=ActionType.resource;resource = Resource.food;amount=0;}
    }

    [Serializable]
    public class TriggerAction: Action {
        public Trigger trigger;
        public bool have;

        public TriggerAction() {type=ActionType.trigger;}
    }

    [Serializable]
    public class DialogAction: Action {
        public int id;

        public DialogAction() {type=ActionType.dialog;}
    }

    private void OnValidate()
    {
        dialogs.ForEach( dialog => {
            var choices = new List<Choice>() {dialog.choiceLeft, dialog.choiceRight};
            choices.ForEach( choice => {
                Action prevData = null;
                choice.actions.ForEach( action => {
                    if (prevData != null && prevData == action.data)
                        action.data = null;
                    switch(action.type)
                    {
                        case ActionType.resource:
                            if (action.data == null || action.data != null && action.data.GetType() != typeof(ResourceAction))
                                action.data = new ResourceAction();
                            break;
                        case ActionType.dialog:
                            if (action.data == null || action.data != null && action.data.GetType() != typeof(DialogAction))
                                action.data = new DialogAction();
                            break;
                        case ActionType.trigger:
                            if (action.data == null || action.data != null && action.data.GetType() != typeof(TriggerAction))
                                action.data = new TriggerAction();
                            break;
                    }
                    prevData = action.data;
                });
            });
            
        });
    }    
}