using UnityEngine;

public class BootController : MonoBehaviour
{
	private void Start()
	{
		SceneController.Instance.StartGame();
	}
}