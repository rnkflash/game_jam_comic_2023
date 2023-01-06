using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveControlMode : ControlMode
{
    private LineRenderer lineRenderer;
    private GameObject uiStuff;
    private NavMeshPath path;

    public override void Start() {
        uiStuff = new GameObject("lineRendererMoveControlMode");
        uiStuff.transform.parent = UI.Instance.markersPath.transform;
        lineRenderer = uiStuff.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        
        path = new NavMeshPath();
    }

    public override void End()
    {
        GameObject.Destroy(uiStuff);
    }

    public override void Update(Ray ray, RaycastHit[] hits, bool unitSelected)
    {
        if (UI.Instance.mouseOver)
            return;

        if (unitSelected)
            return;

        var groundHits = Array.FindAll(hits, h => h.collider.tag == "Ground")
            .Select(g => g.point)
            .ToArray();
        
        if (groundHits.Length > 0) {
                var groundHit = groundHits[0];
                if (Input.GetMouseButtonDown(0)) {
                    if (controller.selectedUnit != null) {
                        controller.selectedUnit.AddAction(new MoveAction(groundHit));
                    }
                }

                if (controller.selectedUnit != null && controller.selectedUnit.currentAction == null) {
                    
                    controller.selectedUnit.agent.CalculatePath(groundHit, path);
                    if (path.status == NavMeshPathStatus.PathComplete)
                        DrawPath(path.corners);
                    else
                        DrawPath(new Vector3[] {controller.selectedUnit.transform.position, groundHit});
                } else
                    ClearPath();
            }
    }

    private void DrawPath(Vector3[] path) {
        lineRenderer.positionCount = path.Length;
        if (path.Length == 0)
            return;
        for (int i = 0; i < path.Length; i++) {
            var corner = path[i];
            lineRenderer.SetPosition(i, new Vector3(corner.x, 0.22f, corner.z));
        }
    }

    private void ClearPath() {
        lineRenderer.positionCount = 0;
    }
}