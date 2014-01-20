// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class RealEstateMarket
{
    public static readonly int MAX_TILES = 7;

    public RealEstateMarket (int p_nb_players)
    {
        m_availables_tiles = new List<TileInstance> ();
        m_tileStacks = new TileStacks (p_nb_players);

        for (int i = 0; i < MAX_TILES; i++) {
            m_availables_tiles.Add (m_tileStacks.PopNextTile ());
        }
    }

    private TileStacks m_tileStacks;
    private List<TileInstance> m_availables_tiles;

    public TileInstance this [int p_index] {
        get {
            return this.m_availables_tiles[p_index];
        }
    }

    public TileInstance RemoveTile (int n)
    {
        if (n < 0 || n > m_availables_tiles.Count) {
            Debug.LogError ("Trying to pick an unbound tile ! Value : " + n);
            return null;
        }

        TileInstance tile = m_availables_tiles [n];
        m_availables_tiles.RemoveAt (n);
        // We may here add a null tile. This is a desired behavior.
        // A null tile is considered has a hole in the real estate market.
        m_availables_tiles.Insert (0, m_tileStacks.PopNextTile ());

        return tile;
    }

    public int PriceOverheadForTileNumber(int p_tile_index)
    {
        if(p_tile_index < 0 || p_tile_index > MAX_TILES)
        {
            Debug.LogError("Asking price overhead for tile number : " + p_tile_index);
            return 0;
        }
        // By convention tile number 0 is the newly arrived tile. So it worth 10$ more than the others.
        int overhead = 10 - p_tile_index * 2;
        return overhead > 0 ? overhead : 0;
    }

    public List<TileInstance> tiles {
        get {
            return this.m_availables_tiles;
        }
    }

    public TileStacks Stacks {
        get {
            return this.m_tileStacks;
        }
    }
}


