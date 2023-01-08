using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Awake() {
        (new Player()).Init();
        Player.Instance.money = 100;
        Player.Instance.fuel = 100;
        Player.Instance.food = 55;
        Player.Instance.onResourcesChanged?.Invoke();
    }

    void Start()
    {
        
    }

    void OnDestroy() {
        Player.Instance.Destroy();
    }
    
    void Update()
    {
        
    }

    public void OnWinClicked() {
        SceneController.Instance.LoadVictoryScene();
    }

    public void OnLoseClicked() {
        SceneController.Instance.LoadLoseScene();
    }
}
