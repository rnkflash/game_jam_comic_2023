using System.Collections.Generic;
using UnityEngine;

namespace game.objects
{
  public class NPCSpawner : MonoBehaviour
  {
    public List<GameObject>  prefabs;

    private void Start()
    {
      InvokeRepeating(nameof(CreateNPC),3f, 3);
    }

    private void CreateNPC()
    {
      Instantiate(prefabs[Random.Range(0, prefabs.Count - 1)], transform);
    }
  }
}