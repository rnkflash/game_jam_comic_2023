using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver = false;
    public GameObject moveTargetMarkerPrefab;
    public GameObject markersPath;

    private static UI _instance;
    public static UI Instance { get { return _instance; } }

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
        PlayerController.Instance.SetMode(new MoveControlMode());
    }

    public void OnMoveButtonPressed() {
        PlayerController.Instance.SetMode(new MoveControlMode());
    }

    public void OnRotateButtonPressed() {
        PlayerController.Instance.SetMode(new RotateControlMode());
    }

    public void OnShootButtonPressed() {
        PlayerController.Instance.SetMode(new RotateControlMode());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
