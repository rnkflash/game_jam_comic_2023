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
    }

    void OnDestroy() {
        DestroyPlayer();
    }
    
    private void CreatePlayer() {
        Player.Instance = new Player();
    }

    private void DestroyPlayer() {
        Player.Instance = null;
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
                    Player.Instance.food = 100;
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

                    if (Player.Instance.distance >= 20000) {
                        state = GameState.Win;
                    } else
                    if (
                        Player.Instance.food <= 0 ||
                        Player.Instance.fuel <= 0 ||
                        Player.Instance.money <= 0
                    ) {
                        state = GameState.Lose;
                    } else
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
