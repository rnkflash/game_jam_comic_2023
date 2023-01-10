using UnityEngine;

namespace StaticData
{
  [CreateAssetMenu(fileName = "CardData", menuName = "Static Data/Card")]
  public class CardStaticData : ScriptableObject
  {
    public CardTypeId CardTypeId;
    public int CardId;
    
    public Sprite sprite;
    [TextArea(10, 100)]
    public string Question;

    [Header("Yes option")]
    public int YesFood;
    public int YesFuel;
    public int YesMoney;
    public float YesDistance;
    [TextArea(5, 100)]
    public string YesAnswer;
    
    [Header("No option")]
    public int NoFood;
    public int NoFuel;
    public int NoMoney;
    public float NoDistance;
    [TextArea(5, 100)]
    public string NoAnswer;
  }
}