using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    public static Cell[,] Grid;

    //--HIDDEN-REFERENCES---------------------------------------------------//

    [SerializeField] private GameObject _cellPrefab;

    //////////////////////////////////////////////////////////////////////////

    public void Assemble(int [,] level)
    {
        int width = level.GetLength(0);
        int height = level.GetLength(1);

        Grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Grid[x, y] = level[x, y] == 1 ?
                    CreateCell(x, y) : null;
            }
        }
        
        transform.localScale = Vector3.one * Mathf.Clamp((3f / (float)height), .25f, 1f);
    }

    public void Clear()
    {
        if (Grid == null) return;

        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Grid[x, y])
                    Destroy(Grid[x, y].gameObject);
            }
        }

        Grid = null;
    }

    private Cell CreateCell(int x, int y)
    {
        Vector2 pos = Vector2.zero;
        pos.x = (x - (Grid.GetLength(0) * .5f) + .5f) * .62f;
        pos.y = (y - (Grid.GetLength(1) * .5f) + .5f) * 1.02f;

        GameObject obj = Instantiate(_cellPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;

        Cell cell = obj.GetComponent<Cell>();

        return cell;
    }

    //////////////////////////////////////////////////////////////////////////
}
