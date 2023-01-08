using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Awake() {
        CreatePlayer();
        EventBus<PlayerDecisionMade>.Sub(OnPlayerDecision);
    }

    void OnDestroy() {
        Player.Instance = null;

        EventBus<PlayerDecisionMade>.Unsub(OnPlayerDecision);
    }
    
    private void CreatePlayer() {
        Player.Instance = new Player();
        Player.Instance.money = 100;
        Player.Instance.fuel = 100;
        Player.Instance.food = 55;

        EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
    }

    private void OnPlayerDecision(PlayerDecisionMade msg) {

        if (msg.decision == PlayerDecision.Yes)
            SceneController.Instance.LoadVictoryScene();

        if (msg.decision == PlayerDecision.No)
            SceneController.Instance.LoadLoseScene();
    }
}
