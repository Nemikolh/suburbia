//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18331
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class RealEstateMarket
{
    public RealEstateMarket (int p_nb_players)
    {
        m_availables_tiles = new List<TileInstance> ();
        m_tileStacks = new TileStacks(p_nb_players);
    }

    private TileStacks m_tileStacks;
    private List<TileInstance> m_availables_tiles;

    public TileInstance Pick (int n)
    {
        if (n < 0 || n > m_availables_tiles.Count) {
            Debug.LogError ("Trying to pick an unbound tile ! Value : " + n);
            return null;
        }

        TileInstance tile = m_availables_tiles [n];
        m_availables_tiles.Remove (tile);
        // We may here add a null tile. This is a desired behavior.
        // A null tile is considered has a hole in the real estate market.
        m_availables_tiles.Insert (0, m_tileStacks.PopNextTile ());

        return tile;
    }
}

