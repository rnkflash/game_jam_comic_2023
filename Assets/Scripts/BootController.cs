using DG.Tweening;
using StaticData;
using UnityEngine;

public class BootController : MonoBehaviour
{
	private void Start()
	{
		DOTween.Init();
		SceneController.Instance.StartGame();
	}
}