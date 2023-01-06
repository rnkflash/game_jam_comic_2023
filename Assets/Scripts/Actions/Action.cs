using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public abstract class Action
{
    protected Unit unit;

    public void Start(Unit unit)
    {
        this.unit = unit;
        OnStart();
    }

    protected abstract void OnStart();

    public void Finish() {
        OnFinish();
        unit = null;
    }

    protected abstract void OnFinish();

    public abstract bool Update();

}
