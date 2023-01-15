using System.Collections;
using System.Collections.Generic;
using System.Linq;
using game;
using UnityEngine;

public enum GameState
{
    Start,
    RandomRoadMove,
    SituationStart,
    Dialog,
    PlayerDecision,
    Aftermath,
    SituationConclusion,
    WinCheck,
    Win,
    Lose,
    Exit
}

public class GameController : MonoBehaviour
{
    private GameState state;
    private SituationStartController situationStartController;
    private DialogController dialogController;
    private PlayerDecisionController playerDecisionController;
    private AftermathController afterMathController;
    private SituationConclusionController situationConclusionController;
    private WinController winController;
    private LoseController loseController;
    private RoadMoveController roadMoveController;

    void Awake() {
        situationStartController = GetComponent<SituationStartController>();
        playerDecisionController = GetComponent<PlayerDecisionController>();
        situationConclusionController = GetComponent<SituationConclusionController>();
        dialogController = GetComponent<DialogController>();
        winController = GetComponent<WinController>();
        loseController = GetComponent<LoseController>();
        afterMathController = GetComponent<AftermathController>();
        roadMoveController = GetComponent<RoadMoveController>();

        state = GameState.Start;
    }

    void OnDestroy() {
        
    }
    
    private IEnumerator Start()
    {
        while (true)
        {
            switch (state)
            {
                case GameState.Start:
                    Player.Instance.SetResource(Resource.money, Balance.values.start_money);
                    Player.Instance.SetResource(Resource.fuel, Balance.values.start_fuel);
                    Player.Instance.SetResource(Resource.food, Balance.values.start_food);
                    Player.Instance.SetResource(Resource.distance, Balance.values.start_distance);

                    EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());
                    EventBus<CarMovedDistance>.Pub(new CarMovedDistance());
                    state = GameState.SituationStart;
                    break;
                case GameState.SituationStart:
                    yield return situationStartController.StartNextSituation();
                    if (Player.Instance.card == null)
                        state = GameState.WinCheck;
                    else
                        state = GameState.Dialog;
                    break;
                case GameState.Dialog:
                    yield return dialogController.StartNextDialogue();
                    state = GameState.PlayerDecision;
                    break;
                case GameState.PlayerDecision:
                    yield return playerDecisionController.StartMakingDecision();
                    state = GameState.Aftermath;
                    break;
                case GameState.Aftermath:
                    if (Player.Instance.GetCurrentChoice().aftermath != "")
                        yield return afterMathController.ShowAftermath();
                    state = GameState.SituationConclusion;
                    break;
                case GameState.SituationConclusion:
                    yield return situationConclusionController.StartConclusion();

                    if (Player.Instance.dialog != -1)
                        state = GameState.Dialog;
                    else
                        state = GameState.RandomRoadMove;

                    break;
                case GameState.RandomRoadMove:
                    yield return roadMoveController.Move();
                    state = GameState.WinCheck;
                    break;
                case GameState.WinCheck:

                    if (WinCheck()) 
                        state = GameState.Win;
                    else if (LooseCheck()) 
                        state = GameState.Lose;
                    else if (Player.Instance.card == null)
                        state = GameState.Lose;
                    else
                        state = GameState.SituationStart;
                    
                    break;
                case GameState.Win:
                    yield return winController.Show();
                    state = GameState.Exit;
                    break;
                case GameState.Lose:
                    yield return loseController.Show();
                    state = GameState.Exit;
                    break;
                case GameState.Exit:
                    SceneController.Instance.LoadMainMenu();
                    yield break;
            }
        }
    }

    private static bool LooseCheck()
    {
        var requiredResources = new List<Resource>() {Resource.food, Resource.fuel};
        return requiredResources.Any(r => Player.Instance.GetResource(r) <= 0);
    }

    private static bool WinCheck()
    {
        return Player.Instance.GetResource(Resource.distance) >= Balance.values.max_distance;
    }
}
