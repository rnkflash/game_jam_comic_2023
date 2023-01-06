using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public NavMeshAgent agent;
    public CharacterController character;
    public GameObject selectionGraphics;
    public GameObject hoverGraphics;
    public GameObject selectCollider;

    private List<Action> actions = new List<Action>();
    public Action currentAction = null;

    void Start()
    {
        selectionGraphics.SetActive(false);
        hoverGraphics.SetActive(false);
    }

    void Update()
    {
        if (currentAction != null) {
            if (!currentAction.Update())
                OnActionComplete();
        } else
        {
            if (actions.Count > 0) {
                currentAction = actions[0];
                actions.RemoveAt(0);
                currentAction.Start(this);        
            }
        }
    }

    public void AddAction(Action action) {
        if (currentAction!= null || actions.Count > 0)
            return;
        actions.Add(action);
    }

    private void OnActionComplete() {
        currentAction.Finish();
        currentAction = null;
    }

    public void Select(bool selected) {
        selectionGraphics.SetActive(selected);
        selectCollider.SetActive(!selected);
    }

    public void Hover(bool hover) {
        hoverGraphics.SetActive(hover);
    }
}
