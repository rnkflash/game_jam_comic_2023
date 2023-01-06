using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class RotateControlMode : ControlMode
{
    public override void Start() {
        if (controller.selectedUnit != null) {
            OnUnitSelected();
        }
    }

    private void OnUnitSelected() {

        controller.canSelectUnit = false;
        controller.canHoverUnit = false;
    }    

    public override void End()
    {
        controller?.selectedUnit?.character?.animator?.SetBool("IsAiming", false);
        controller.canSelectUnit = true;
        controller.canHoverUnit = true;
    }

    public override void Update(Ray ray, RaycastHit[] hits, bool unitSelected)
    {
        if (UI.Instance.mouseOver)
            return;

        if (unitSelected) {
            OnUnitSelected();
            return;
        }

        if (controller.selectedUnit == null)
            return;
        
        controller.selectedUnit.character.animator.SetBool("IsAiming", true);

        if (hits.Length > 0) {
            string[] shootableTags = { "UnitBodyPart", "Ground" };
            var shootableHits = System.Array.FindAll(hits, h => shootableTags.Contains(h.collider.tag));
            if (shootableHits.Length > 0) {
                System.Array.Sort<RaycastHit>(
                    shootableHits,
                    new System.Comparison<RaycastHit>((i1, i2) => {
                            var d1 = Vector3.Distance(ray.origin, i1.point); 
                            var d2 = Vector3.Distance(ray.origin, i2.point); 
                            if (d1 == d2)
                                return 0;
                            else 
                            if (d2 > d1)
                                return -1;
                            else
                                return 1;
                        }
                    )
                );

                foreach(RaycastHit raycastHit in shootableHits) {
                    Debug.DrawLine(
                        controller.selectedUnit.character.weapon.raycastOrigin.position, 
                        raycastHit.point, 
                        new Color(Random.Range(0.5f,1.0f), Random.Range(0.5f,1.0f), Random.Range(0.5f,1.0f))
                        );
                }

                var hit = shootableHits[0];
                controller.selectedUnit.character.LookAt(hit.point);

                if (Input.GetMouseButtonDown(0)) {
                    controller.selectedUnit.AddAction(new FireAction(2 * 3.14f));
                }
            }
        }
    }
}