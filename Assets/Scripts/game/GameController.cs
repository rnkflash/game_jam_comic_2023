using System.Collections;
using System.Collections.Generic;
using game;
using UnityEngine;

public enum GameState
{
    Start,
    SituationStart,
    Dialog,
    PlayerDecision,
    SituationConclusion,
    Win,
    Lose,
    Exit
}

public class GameController : MonoBehaviour
{
    private GameState state;
    public SituationStartController situationStartController;
    public PlayerDecisionController playerDecisionController;
    public SituationConclusionController situationConclusionController;
    public DialogController dialogController;
    public WinController winController;
    public LoseController loseController;
    private int dialogNumber = 0;

    void Awake() {
        Player.Instance = new Player();
        state = GameState.Start;
    }

    void OnDestroy() {
        Player.Instance = null;
    }
    
    private IEnumerator Start()
    {
        while (true)
        {
            switch (state)
            {
                case GameState.Start:
                    Player.Instance.money = 50;
                    Player.Instance.fuel = 50;
                    Player.Instance.food = 50;
                    Player.Instance.distance = 0;
                    EventBus<PlayerResourcesChanged>.Pub(new PlayerResourcesChanged());

                    state = GameState.SituationStart;
                    break;
                case GameState.SituationStart:
                    yield return situationStartController.StartNextSituation();
                    state = GameState.Dialog;
                    break;
                case GameState.Dialog:
                    yield return dialogController.StartNextDialogue(dialogNumber);
                    state = GameState.PlayerDecision;
                    break;
                case GameState.PlayerDecision:
                    yield return playerDecisionController.StartMakingDecision(dialogNumber);
                    state = GameState.SituationConclusion;
                    break;
                case GameState.SituationConclusion:
                    yield return situationConclusionController.StartConclusion(dialogNumber);

                    if (WinCheck()) state = GameState.Win;
                    else if (LooseCheck()) state = GameState.Lose;
                    else if (playerDecisionController.DialogNotFinished)
                    {
                        dialogNumber++;
                        state = GameState.Dialog;
                    }
                    else
                    {
                        dialogNumber = 0;
                        state = GameState.SituationStart;
                    }
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
        return Player.Instance.food <= 0 ||
               Player.Instance.fuel <= 0 ||
               Player.Instance.money <= 0;
    }

    private static bool WinCheck()
    {
        return Player.Instance.distance >= 20000;
    }
}
