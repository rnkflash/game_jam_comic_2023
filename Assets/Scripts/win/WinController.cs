using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("victory"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked() {
        SceneController.Instance.LoadMainMenu();
    }
}
