using System;
using System.Collections.Generic;
using UnityEngine;

/*

Here's how we calculate position :
    - The first 3 tiles are in column 0 (x=0)
    - By descending order, they are in rows 0, 2 and 4 (y=0, 2, 4)
Hence the tile between the suburbs and the park, on the right will be in position :
    x = 1, y = 1

*/

public class TilePosition : System.Object
{
    private readonly int m_x;
    private readonly int m_y;

    public TilePosition(int p_x, int p_y)
    {
        if (!IsValidPosition(p_x, p_y))
            Debug.Log ("TilePosition is incorrect!");

        m_x = p_x;
        m_y = p_y;
    }

    public bool IsAdjacentTo(TilePosition p_pos)
    {
        if (p_pos.x == this.x)
        {
            // Directly above/below tile
            if (p_pos.y == this.y - 2 || p_pos.y == this.y + 2)
                return true;
            return false;
        }
        if (p_pos.x == this.x - 1 || p_pos.x == this.x + 1)
        {
            // Side tile
            if (p_pos.y == this.y - 1 || p_pos.y == this.y + 1)
                return true;
            return false;
        }
        return false;
    }

    public int x {
        get {
            return this.m_x;
        }
    }

    public int y {
       get {
            return this.m_y;
        }
    }

    public static bool IsValidPosition(int p_x, int p_y)
    {
        // If y is positive and x and y share the same parity, it's good
        if (p_y >= 0 && (p_x + p_y) % 2 == 0)
            return true;
        return false;
    }

    delegate void func (List<TilePosition> p_list, int x_, int y_);

    public List<TilePosition> GetAdjacentPositions()
    {
        List<TilePosition> adjacent_pos = new List<TilePosition>();
        int x, y;

        func AddIfValid = (List<TilePosition> p_list, int x_, int y_) =>
        {
            if (TilePosition.IsValidPosition(x_, y_))
                adjacent_pos.Add(new TilePosition(x_, y_));
        };

        // Above
        x = this.x;
        y = this.y - 2;
        AddIfValid(adjacent_pos, x, y);
        // Below
        x = this.x;
        y = this.y + 2;
        AddIfValid(adjacent_pos, x, y);
        // Top left
        x = this.x - 1;
        y = this.y - 1;
        AddIfValid(adjacent_pos, x, y);
        // Bottom left
        x = this.x - 1;
        y = this.y + 1;
        AddIfValid(adjacent_pos, x, y);
        // Top right
        x = this.x + 1;
        y = this.y - 1;
        AddIfValid(adjacent_pos, x, y);
        // Bottom right
        x = this.x + 1;
        y = this.y + 1;
        AddIfValid(adjacent_pos, x, y);

        return adjacent_pos;
    }

    public static implicit operator Vector3 (TilePosition p_pos)
    {
        return new Vector3(p_pos.x, p_pos.y, 0);
    }

    public override bool Equals(System.Object p_obj)
    {
        if (p_obj == null)
        {
            return false;
        }

        TilePosition p_pos = p_obj as TilePosition;
        if ((System.Object) p_pos == null)
        {
            return false;
        }

        return (this.x == p_pos.x) && (this.y == p_pos.y);
    }
}

