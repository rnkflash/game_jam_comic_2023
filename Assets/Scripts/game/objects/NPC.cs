using System;
using DG.Tweening;
using UnityEngine;

namespace game.objects
{
  public class NPC : MonoBehaviour
  {
    private void Start()
    {
      transform.DOMoveX(300, 600).OnComplete(()=>Destroy(gameObject));
      
    }

    private void OnDestroy()
    {
      transform.DOKill();
    }
  }
}