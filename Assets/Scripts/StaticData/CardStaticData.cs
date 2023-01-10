using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
  [CreateAssetMenu(fileName = "CardData", menuName = "Static Data/Card")]
  public class CardStaticData : ScriptableObject
  {
    public CardTypeId CardTypeId;
    public int CardId;
    public Sprite sprite;

    public List<DialogSegment> DialogSegments;

  }
}