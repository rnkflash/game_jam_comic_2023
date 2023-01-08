using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameState state = GameState.Start;
    public SituationStartController situationStartController;
    public PlayerDecisionController playerDecisionController;
    public SituationConclusionController situationConclusionController;

    void Awake() {
        CreatePlayer();
        EventBus<PlayerDecisionMade>.Sub(OnPlayerDecision);
    }

    void OnDestroy() {
        DestroyPlayer();
        EventBus<PlayerDecisionMade>.Unsub(OnPlayerDecision);
    }
    
    private void CreatePlayer() {
        Player.Instance = new Player();
    }

    private void DestroyPlayer() {
        Player.Instance = null;
    }

    private void OnPlayerDecision(PlayerDecisionMade msg) {

        if (msg.decision == PlayerDecision.Yes)
            SceneController.Instance.LoadVictoryScene();

        if (msg.decision == PlayerDecision.No)
            SceneController.Instance.LoadLoseScene();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            switch (state)
            {
                case GameState.Start:
                    Player.Instance.money = 100;
                    Player.Instance.fuel = 100;
                    Player.Instance.food = 55;
                    Player.Instance.distance = 0;
                    EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());

                    state = GameState.SituationStart;
                    break;
                case GameState.SituationStart:
                    yield return situationStartController.StartNextSituation();
                    state = GameState.PlayerDecision;
                    break;
                case GameState.PlayerDecision:
                    yield return playerDecisionController.StartMakingDecision();
                    state = GameState.SituationConclusion;
                    break;
                case GameState.SituationConclusion:
                    yield return situationConclusionController.StartConclusion();
                    state = GameState.SituationStart;
                    break;
                case GameState.Win:
                    SceneController.Instance.LoadVictoryScene();
                    yield break;
                case GameState.Lose:
                    SceneController.Instance.LoadLoseScene();
                    yield break;
            }
        }
    }
}
