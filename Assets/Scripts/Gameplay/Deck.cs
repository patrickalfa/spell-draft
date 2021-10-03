using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    [Header("Interpolation")]
    public float spacing;
    public float interpolationSpeed;

    //----------------------------------------------------------------------//

    public static List<Card> Cards;

    //----------------------------------------------------------------------//

    [SerializeField] private GameObject _cardPrefab;

    //--GETTERS-&-SETTERS---------------------------------------------------//

    public static bool IsEmpty
    {
        get { return Cards.Count == 0; }
    }

    public static Card TopCard
    {
        get { return Cards[0]; }
    }

    //////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        if (GameManager.State != GAME_STATE.GAME || Cards == null)
            return;

        for (int i = 0; i < Cards.Count; i++)
            Cards[i].transform.localPosition = GetCardInterpolatedPosition(i);
    }

    //----------------------------------------------------------------------//

    public static void DrawNext()
    {
        if (!TopCard) return;

        Cards.RemoveAt(0);

        if (!IsEmpty)
            TopCard.FaceUp = true;
    }

    //----------------------------------------------------------------------//

    public void Assemble(ELEMENT[] deck)
    {
        Cards = new List<Card>(deck.Length);

        // Create cards.
        for (int i = 0; i < deck.Length; i++)
            Cards.Add(CreateCard(deck[i]));

        // Initialize cards.
        for (int i = 0; i < deck.Length; i++)
        {
            Cards[i].transform.localPosition = GetCardPosition(i);
            Cards[i].OrderInDeck = i;
        }

        // Set top card face up.
        TopCard.FaceUp = true;
    }

    public void Clear()
    {
        if (Cards == null) return;

        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i])
                Destroy(Cards[i].gameObject);
        }

        Cards.Clear();
    }

    private Card CreateCard(ELEMENT type)
    {
        GameObject obj = Instantiate(_cardPrefab, Vector3.zero, Quaternion.Euler(0f, 180f, 0f), transform);
        Card card = obj.GetComponent<Card>();
        card.Type = type;

        return card;
    }

    private Vector3 GetCardInterpolatedPosition(int index)
    {
        return Vector3.Lerp(
            Cards[index].transform.localPosition,
            GetCardPosition(index),
            Time.deltaTime * interpolationSpeed
        );
    }

    private Vector3 GetCardPosition(int index)
    {
        return Vector3.right * ((Cards.Count * .5f * spacing) - (index * spacing) - (spacing * .5f));
    }

    //////////////////////////////////////////////////////////////////////////
}
