using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public GameObject cardBackPrefab;
    public GameObject cardBack;
    public GameObject cardFront;

    private bool noCards = true;

    void Start() {
        noCards = true;
        cardBack.SetActive(!noCards);
        cardFront.SetActive(false);
    }

    public void FlipFrontCard(Sprite faceImg, bool lastCard = false) {
        var flipTime = 0.25f;

        var img = cardFront.GetComponent<Image>();
        img.sprite = faceImg;
        cardFront.SetActive(true);
        cardBack.SetActive(!lastCard);
        noCards = lastCard;

        var pbj = Instantiate(cardBackPrefab, cardBack.transform.position, Quaternion.identity, transform);
        pbj.transform.SetAsLastSibling();

        cardFront.transform.localScale = new Vector3(0, 1, 1);

        pbj.transform.DOScaleX(0.0f, flipTime/2).SetEase(Ease.InCubic).OnComplete(()=>{
            Destroy(pbj);
            cardFront.transform.DOScaleX(1.0f, flipTime/2).SetEase(Ease.OutCubic);
        });

        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("flip_card"));
    }

    public void AddCards(int amount) {
        var appearingDelay = 0.05f;
        var movingDelay = 0.1f;
        var movingDuration = 0.7f;

        for (int i = 0; i < amount; i++) 
        {
            var pbj = Instantiate(cardBackPrefab, new Vector3(-400,200,0), Quaternion.identity);
            pbj.transform.SetParent(transform, false);
            pbj.transform.SetAsFirstSibling();

            var image = pbj.GetComponentInChildren<Image>();
            var color = image.color;
            color.a = 0.0f;
            image.color = color;
            image.DOFade(1.0f, appearingDelay).SetDelay(i * movingDelay).OnComplete(()=>{
                SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("deal_card"));
            });

            pbj.transform.DOMove(cardBack.transform.position, movingDuration).SetDelay(i * movingDelay).SetEase(Ease.OutCubic).OnComplete(()=> {
                Destroy(pbj);
                if (noCards)
                {
                    noCards = false;
                    cardBack.SetActive(true);
                }
            });
        }
        
    }

    public void LeanRight() {
        cardFront.transform.DOKill();
        cardFront.transform.DORotate(new Vector3(0,0,-30), 0.25f);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("lean_card"));
    }

    public void LeanLeft() {
        cardFront.transform.DOKill();
        cardFront.transform.DORotate(new Vector3(0,0,30), 0.25f);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("lean_card"));
    }

    public void StopLeaning() {
        cardFront.transform.DOKill();
        cardFront.transform.DORotate(new Vector3(0,0,0), 0.25f);
        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("lean_card"));
    }

    public void FallDownLeft() {
        FallDown(30, -200);
    }

    public void FallDownRight() {
        FallDown(-30, 200);
    }

    private void FallDown(float angle, float x) {
        cardFront.transform.DOKill();
        var clone = Instantiate(cardFront, Vector3.zero, Quaternion.identity);
        clone.transform.SetParent(transform, false);
        clone.transform.SetAsLastSibling();

        cardFront.transform.rotation = Quaternion.identity;
        cardFront.SetActive(false);

        clone.transform.DOLocalMoveX(x, 0.25f);
        clone.transform.DOLocalMoveY(-700.0f, 0.25f).SetEase(Ease.InCubic);
        clone.transform.DOLocalRotate(new Vector3(0,0,angle), 0.25f);

        var image = clone.GetComponentInChildren<Image>();
        image.DOFade(0.0f, 0.26f).SetDelay(0.1f).OnComplete(()=>{
            Destroy(clone);
        });

        SoundSystem.PlaySound(Sounds.Instance.GetAudioClip("fall_card"));
    }

}
