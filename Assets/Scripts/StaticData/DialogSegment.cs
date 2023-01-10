using UnityEngine;

namespace StaticData
{
  [System.Serializable]
  public class DialogSegment
  {
    [TextArea(5, 100)]
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