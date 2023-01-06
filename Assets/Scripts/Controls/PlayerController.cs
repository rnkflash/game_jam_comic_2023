using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    public new Camera camera;
    private string[] defaultRaycastLayers = new string[] {"NavMeshLayer", "Units"};
    private int raycastLayerMask;
    private ControlMode currentMode = null;
    public Unit selectedUnit = null;
    public Unit hoveredUnit = null;
    public bool canSelectUnit = true;
    public bool canHoverUnit = true;

    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start()
    {
        raycastLayerMask = LayerMask.GetMask(defaultRaycastLayers);
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100f, raycastLayerMask);
        Array.Sort<RaycastHit>(
            hits,
            new Comparison<RaycastHit>((i1, i2) => {
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

        var units = Array.FindAll(hits, h => h.collider.tag == "UnitSelectCollider")
            .Select(r => r.transform.GetComponentInParent<Unit>())
            .Where(x => x != null)
            .ToArray();
        bool unitSelected = false;
        Unit unit = null;

        if (units.Length > 0) {
            unit = units[0];
        }

        if (canSelectUnit && Input.GetMouseButtonDown(0) && unit != null) {
                if (selectedUnit != unit) {
                    if (selectedUnit != null)
                        selectedUnit.Select(false);
                    selectedUnit = unit;
                    selectedUnit.Select(true);

                    unitSelected = true;
                }
        }

        if (canHoverUnit) {
            if ((hoveredUnit != unit || unit == null) && hoveredUnit != null) {
                hoveredUnit.Hover(false);
                hoveredUnit = null;
            }

            if (hoveredUnit == null && unit != null) {
                hoveredUnit = unit;
                hoveredUnit.Hover(true);
            }
        } else {
            if (hoveredUnit != null)
                hoveredUnit.Hover(false);
            hoveredUnit = null;
        }

        if (currentMode != null)
            currentMode.Update(ray, hits, unitSelected);
    }

    public void DebugRay(Vector3 origin, Vector3 direction, Color color, float duration = 0.5f) {
        Debug.DrawRay(origin, direction, color, duration);
    }

    public void SetMode(ControlMode mode) {
        if (currentMode != null) {
            currentMode.End();
            currentMode.controller = null;
        }
        currentMode = mode;
        currentMode.controller = this;
        currentMode.Start();
    }
}
