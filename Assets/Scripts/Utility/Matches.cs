using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Matches
{
    //////////////////////////////////////////////////////////////////////////

    public static int CheckMatches(int x, int y)
    {
        if (!CellGrid.Grid[x, y] || CellGrid.Grid[x, y].Element == null) return 0;

        int score = 0;

        switch (CellGrid.Grid[x, y].Element)
        {
            case ELEMENT.WATER: score = CheckWaterMatches(x, y); break;
            case ELEMENT.EARTH: score = CheckEarthMatches(x, y); break;
            case ELEMENT.WIND: score = CheckWindMatches(x, y); break;
            case ELEMENT.FIRE: score = CheckFireMatches(x, y); break;
        }

        CellGrid.Grid[x, y].Card.Points = score;

        if (score > 0)
            CellGrid.Grid[x, y].transform.rotation =
                Quaternion.Euler(0f, 0f, Random.Range(-100f, 100f) * Time.deltaTime);
        else
            CellGrid.Grid[x, y].transform.rotation = Quaternion.identity;

        return score;
    }

    //- WATER --------------------------------------------------------------//

    public static int CheckWaterMatches(int x, int y)
    {
        return CheckWaterFireAdjacent(x, y);
    }

    private static int CheckWaterFireAdjacent(int x, int y)
    {
        int gridWidth = CellGrid.Grid.GetLength(0);
        int gridHeight = CellGrid.Grid.GetLength(1);

        int count = 0;

        if (x > 0 && IsAdjacentToFire(x - 1, y, true))
        {
            count++;
            CellGrid.Grid[x - 1, y].Card.FaceUp = false;
        }
        if (x < gridWidth - 1 && IsAdjacentToFire(x + 1, y, true))
        {
            count++;
            CellGrid.Grid[x + 1, y].Card.FaceUp = false;
        }
        if (y > 0 && IsAdjacentToFire(x, y - 1, true))
        {
            count++;
            CellGrid.Grid[x, y - 1].Card.FaceUp = false;
        }
        if (y < gridHeight - 1 && IsAdjacentToFire(x, y + 1, true))
        {
            count++;
            CellGrid.Grid[x, y + 1].Card.FaceUp = false;
        }

        return count * 2;
    }

    //- EARTH --------------------------------------------------------------//

    public static int CheckEarthMatches(int x, int y)
    {
        return CheckEarthWaterAdjacent(x, y);
    }

    private static int CheckEarthWaterAdjacent(int x, int y)
    {
        int gridWidth = CellGrid.Grid.GetLength(0);
        int gridHeight = CellGrid.Grid.GetLength(1);

        int count = 0;

        if (x > 0 && IsAdjacentToWater(x - 1, y)) count++;
        if (x < gridWidth - 1 && IsAdjacentToWater(x + 1, y)) count++;
        if (y > 0 && IsAdjacentToWater(x, y - 1)) count++;
        if (y < gridHeight - 1 && IsAdjacentToWater(x, y + 1)) count++;

        return count * 2;
    }

    //- WIND ---------------------------------------------------------------//

    public static int CheckWindMatches(int x, int y)
    {
        return CheckWindNoAdjacent(x, y);
    }

    private static int CheckWindNoAdjacent(int x, int y)
    {
        int gridWidth = CellGrid.Grid.GetLength(0);
        int gridHeight = CellGrid.Grid.GetLength(1);

        int count = 0;

        if (x > 0 && IsAdjacentToWind(x - 1, y)) count++;
        if (x < gridWidth - 1 && IsAdjacentToWind(x + 1, y)) count++;
        if (y > 0 && IsAdjacentToWind(x, y - 1)) count++;
        if (y < gridHeight - 1 && IsAdjacentToWind(x, y + 1)) count++;

        return count == 0 ? 2 : 0;
    }

    //- FIRE ---------------------------------------------------------------//

    public static int CheckFireMatches(int x, int y)
    {
        return CheckFire2Adjacent(x, y);
    }

    private static int CheckFire2Adjacent(int x, int y)
    {
        if (!CellGrid.Grid[x, y].Card.FaceUp) return 0;

        int gridWidth = CellGrid.Grid.GetLength(0);
        int gridHeight = CellGrid.Grid.GetLength(1);
        
        int count = 0;

        if (x > 0 && IsAdjacentToFire(x - 1, y)) count++;
        if (x < gridWidth - 1 && IsAdjacentToFire(x + 1, y)) count++;
        if (y > 0 && IsAdjacentToFire(x, y - 1)) count++;
        if (y < gridHeight - 1 && IsAdjacentToFire(x, y + 1)) count++;

        return count > 1 ? 3 : 0;
    }

    //- UTILS --------------------------------------------------------------//

    private static bool IsAdjacentToFire(int x, int y, bool checkFaceDown = false)
    {
        return CellGrid.Grid[x, y] &&
            CellGrid.Grid[x, y].Element == ELEMENT.FIRE &&
            (checkFaceDown || CellGrid.Grid[x, y].Card.FaceUp);
    }

    private static bool IsAdjacentToWater(int x, int y, bool checkFaceDown = false)
    {
        return CellGrid.Grid[x, y] &&
            CellGrid.Grid[x, y].Element == ELEMENT.WATER &&
            (checkFaceDown || CellGrid.Grid[x, y].Card.FaceUp);
    }

    private static bool IsAdjacentToWind(int x, int y, bool checkFaceDown = false)
    {
        return CellGrid.Grid[x, y] &&
            CellGrid.Grid[x, y].Element == ELEMENT.WIND &&
            (checkFaceDown || CellGrid.Grid[x, y].Card.FaceUp);
    }

    //////////////////////////////////////////////////////////////////////////
}
