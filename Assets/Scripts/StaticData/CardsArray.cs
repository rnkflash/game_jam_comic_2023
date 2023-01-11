using UnityEngine;

namespace StaticData
{
  public class CardsArray: Singleton<CardsArray>
  {
    public CardStaticData[] CardsLv1;
    public CardStaticData[] CardsLv2;
    public CardStaticData[] CardsLv3;
    public ReignsTypeCard[] newCards;

    protected override void Created()
    {
      base.Created();
      
      CardsLv1 = Resources.LoadAll<CardStaticData>("Cards/EventCardsLv1");
      CardsLv2 = Resources.LoadAll<CardStaticData>("Cards/EventCardsLv2");
      CardsLv3 = Resources.LoadAll<CardStaticData>("Cards/EventCardsLv3");
      newCards = Resources.LoadAll<ReignsTypeCard>("Cards/ReignsTypeCards");
    }
  }
}