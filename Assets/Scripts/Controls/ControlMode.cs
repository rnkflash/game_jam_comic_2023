using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public abstract class ControlMode {
    public PlayerController controller;
    public abstract void Update(Ray ray, RaycastHit[] hits, bool unitSelected);
    public virtual void Start() {}
    public virtual void End() {}
}