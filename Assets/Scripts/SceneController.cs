using System.Threading.Tasks;

using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
	public async void StartGame()
	{
		await LoadSceneAsync("menu", LoadSceneMode.Single);
	}

	public async void LoadMainMenu()
	{
		await LoadSceneAsync("menu", LoadSceneMode.Single);
	}

	public async void LoadGameplay()
	{
		await LoadSceneAsync("game", LoadSceneMode.Single);
	}

	private async Task LoadSceneAsync(string sceneName, LoadSceneMode mode)
	{
		var loadOp = SceneManager.LoadSceneAsync(sceneName, mode);

		while (!loadOp.isDone)
		{
			await Task.Delay(60);
		}
	}
}