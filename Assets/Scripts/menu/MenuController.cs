using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundSystem.ChangeTrack(Sounds.Instance.GetAudioClip("georgian_folk_rap"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartClicked() {
        SceneController.Instance.LoadGameplay();
    }
}
