using System;
using System.Collections.Generic;

public class Player
{
    public static Player Instance;
    public void Init() { Instance = this; }
    public void Destroy() { if (Instance == this) Instance = null; }

    public Action onResourcesChanged;
    public Action onTriggersChanged;

    public int money = 100;
    public int food = 99;
    public int fuel = 100;

    public List<string> triggers = new List<string>();

    public void AddMoney(int amount) {
        money += amount;
        onResourcesChanged?.Invoke();
    }

    public void AddFood(int amount) {
        food += amount;
        onResourcesChanged?.Invoke();
    }

    public void AddFuel(int amount) {
        fuel += amount;
        onResourcesChanged?.Invoke();
    }

    public void AddTrigger(string trigger) {
        triggers.Add(trigger);
        onTriggersChanged?.Invoke();
    }

}