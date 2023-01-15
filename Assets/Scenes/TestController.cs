using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject cardsObject;
    private Cards cards;

    void Start()
    {
        cards = cardsObject.GetComponent<Cards>();

        

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(2.0f);
        cards.AddCards(10);

        yield return new WaitForSeconds(2.0f);
        cards.FlipFrontCard(Characters.Instance.GetCharacterCard(Character.Барыга).image);

        yield return new WaitForSeconds(2.0f);
        cards.LeanRight();

        yield return new WaitForSeconds(2.0f);
        cards.FallDownRight();

        yield return new WaitForSeconds(2.0f);
        cards.FlipFrontCard(Characters.Instance.GetCharacterCard(Character.Айтишник).image);

        yield return new WaitForSeconds(2.0f);
        cards.LeanLeft();

        yield return new WaitForSeconds(2.0f);
        cards.FallDownLeft();

        yield return new WaitForSeconds(2.0f);
        cards.FlipFrontCard(Characters.Instance.GetCharacterCard(Character.Зетовиц).image, true);

        yield return new WaitForSeconds(2.0f);
        cards.FallDownLeft();

        yield return new WaitForSeconds(2.0f);
        cards.AddCards(1);

        yield return new WaitForSeconds(2.0f);
        cards.FlipFrontCard(Characters.Instance.GetCharacterCard(Character.Журналистпропагандон).image, true);

        yield return new WaitForSeconds(2.0f);
        cards.LeanLeft();

        yield return new WaitForSeconds(0.12f);
        cards.LeanRight();

        yield return new WaitForSeconds(0.12f);
        cards.FallDownRight();
    }

    void Update()
    {
        
    }
}
