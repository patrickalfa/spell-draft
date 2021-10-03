using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Level
{
    public string title;

    // LEVEL GRIDS
    // 1 = Free cell
    // 0 = Locked cell
    public int[,] grid;

    // LEVEL DECKS
    // List of elements:
    // Water, earth, wind and fire
    public ELEMENT[] deck;

    // LEVEL SCORES
    // Minimum score required to complete level
    public int minScore;
    // Maximum score allowed to complete level
    public int maxScore;

    public Level(string title, int[,] grid, ELEMENT[] deck, int minScore, int maxScore)
    {
        this.title = title;
        this.grid = grid;
        this.deck = deck;
        this.minScore = minScore;
        this.maxScore = maxScore;
    }
}

public class Data : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    public static Level[] Levels =
    {
        // LEVEL 1
        new Level(
            title: "I",
            grid: new int[2, 2]
            {
                { 1, 1 },
                { 1, 1 },
            },
            deck: new ELEMENT[4]
            {
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.EARTH,
                ELEMENT.WATER
            },
            minScore: 2,
            maxScore: 4
        ),
        // LEVEL 2
        new Level(
            title: "II",
            grid: new int[3, 3]
            {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            },
            deck: new ELEMENT[9]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 8,
            maxScore: 10
        ),
        // LEVEL 3
        new Level(
            title: "III",
            grid: new int[5, 3]
            {
                { 0, 1, 0 },
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 0, 1, 0 }
            },
            deck: new ELEMENT[11]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 12,
            maxScore: 15
        ),
        // LEVEL 4
        new Level(
            title: "IV",
            grid: new int[5, 3]
            {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            },
            deck: new ELEMENT[15]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 20,
            maxScore: 25
        ),
        // LEVEL 5
        new Level(
            title: "V",
            grid: new int[5, 3]
            {
                { 1, 1, 1 },
                { 1, 0, 1 },
                { 1, 1, 1 },
                { 1, 0, 1 },
                { 1, 1, 1 }
            },
            deck: new ELEMENT[13]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 16,
            maxScore: 20
        ),
        // LEVEL 6
        new Level(
            title: "VI",
            grid: new int[4, 4]
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            deck: new ELEMENT[16]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 24,
            maxScore: 28
        ),
        // LEVEL 7
        new Level(
            title: "VII",
             grid: new int[5, 4]
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            deck: new ELEMENT[16]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 20,
            maxScore: 24
        ),
        // LEVEL 8
        new Level(
            title: "VIII",
            grid: new int[5, 4]
            {
                { 1, 1, 1, 1 },
                { 1, 0, 1, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 1 }
            },
            deck: new ELEMENT[18]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 26,
            maxScore: 30
        ),
        // LEVEL 9
        new Level(
            title: "IX",
            grid: new int[5, 5]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 0, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 0, 1, 1 },
                { 1, 1, 1, 1, 1 }
            },
            deck: new ELEMENT[20]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 30,
            maxScore: 34
        ),
        // LEVEL 10
        new Level(
            title: "X",
            grid: new int[7, 5]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 0, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            },
            deck: new ELEMENT[28]
            {
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.WATER,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.EARTH,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.WIND,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE,
                ELEMENT.FIRE
            },
            minScore: 45,
            maxScore: 50
        ),
    };

    //----------------------------------------------------------------------//

    public static ELEMENT[] ShuffleDeck(ELEMENT[] deck)
    {
        System.Random rng = new System.Random();
        return deck.OrderBy(x => rng.Next()).ToArray();
    }

    //////////////////////////////////////////////////////////////////////////
}