using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    public Color onColor, offColor;
    public Sprite cellSprite;

    //----------------------------------------------------------------------//

    private SpriteRenderer _sprt;

    //--GETTERS-&-SETTERS---------------------------------------------------//

    public ELEMENT? Element { get; private set; } = null;
    public Card Card { get; private set; } = null;

    //////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        _sprt = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (Deck.IsEmpty || Element != null) return;

        _sprt.sprite = GameManager.Elements[(int)Deck.TopCard.Type].FaceSprite;
        _sprt.color = onColor;
    }

    private void OnMouseExit()
    {
        if (Deck.IsEmpty || Element != null) return;

        _sprt.sprite = cellSprite;
        _sprt.color = offColor;
    }

    private void OnMouseUpAsButton()
    {
        if (Deck.IsEmpty || Element != null) return;

        Element = Deck.TopCard.Type;
        _sprt.sprite = GameManager.Elements[(int)Element].FaceSprite;
        _sprt.color = onColor;

        Deck.TopCard.OrderInDeck = Deck.Cards.Count + 1;
        Deck.TopCard.transform.DOMove(transform.position, .25f);
        Deck.TopCard.transform.parent = transform;
        Card = Deck.TopCard;

        SoundManager.PlaySound(GameManager.Elements[(int)Card.Type].sound);

        Deck.DrawNext();
    }

    //////////////////////////////////////////////////////////////////////////
}
