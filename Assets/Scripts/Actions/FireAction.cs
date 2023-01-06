using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class FireAction : Action
{
    private float angle;
    private float timePassed;

    public FireAction(float angle) {
        this.angle = angle;
    }

    protected override void OnStart()
    {
        unit.character.animator.SetBool("IsFiring", true);
        timePassed = 0f;
    }

    protected override void OnFinish()
    {
        unit.character.animator.SetBool("IsFiring", false);
    }

    public override bool Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 1f)
            return false;

        return true;
    }

}