using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveAction: Action
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private GameObject moveTargetMarker;
    private GameObject lineRendererMarker;
    private LineRenderer lineRenderer;

    public MoveAction(Vector3 destination) {
        this.destination = destination;
    }

    protected override void OnStart()
    {
        agent = unit.agent;
        unit.character.MoveTo(destination);

        moveTargetMarker = GameObject.Instantiate(UI.Instance.moveTargetMarkerPrefab, destination, Quaternion.identity, UI.Instance.markersPath.transform);
        
        lineRendererMarker = new GameObject("lineRendererMoveAction");
        lineRendererMarker.transform.parent = UI.Instance.markersPath.transform;
        lineRenderer = lineRendererMarker.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    protected override void OnFinish() {
        unit = null;
        agent = null;

        GameObject.Destroy(moveTargetMarker);
        GameObject.Destroy(lineRendererMarker);
    }

    public override bool Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance) {
            DrawPath(agent.path.corners);
        }
        else {
            return false;
        }

        return true;
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
}
