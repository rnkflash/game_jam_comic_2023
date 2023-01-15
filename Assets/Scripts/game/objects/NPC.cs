using System;
using DG.Tweening;
using UnityEngine;

namespace game.objects
{
  public class NPC : MonoBehaviour
  {
    private void Start()
    {
      transform.DOMoveX(100, 200).OnComplete(()=>Destroy(gameObject));
      
    }

    private void OnDestroy()
    {
      transform.DOKill();
    }
  }
}