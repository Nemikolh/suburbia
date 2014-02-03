// --------------------------------------------------------------- //
//
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System.IO;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class TileStacks
{
    private Stack<TileInstance> m_tilesA;
    private Stack<TileInstance> m_tilesB;
    private Stack<TileInstance> m_tilesC;
    private List<Tile> m_tiles_availables;

    public TileStacks (int p_nb_players)
    {
        m_tilesA = new Stack<TileInstance> ();
        m_tilesB = new Stack<TileInstance> ();
        m_tilesC = new Stack<TileInstance> ();

        m_tiles_availables = new List<Tile> ();

        TextAsset json_data = Resources.Load("Data/tiles") as TextAsset;

        if(json_data.text.Length > 0)
        {
            JSONArray tiles = (JSONArray)JSON.Parse (json_data.text);

            // Debug.Log (tiles.Count + " tiles to load from JSON.");

            foreach (JSONNode child in tiles) {
                // We load each tile.
                m_tiles_availables.Add (Tile.LoadFromJson (child.AsObject));
            }

            // All tiles types have been instantiated
            // Debug.Log (m_tiles_availables.Count + " tiles have been loaded.");
        }

        foreach (Tile tile in m_tiles_availables) {

            Stack<TileInstance> tiles_stack;

            switch (tile.letter) {
            case ETileLetter.A:
                tiles_stack = m_tilesA;
                break;
            case ETileLetter.B:
                tiles_stack = m_tilesB;
                break;
            case ETileLetter.C:
                tiles_stack = m_tilesC;
                break;
            default:
                continue;
            }

            for (int i = 0; i < tile.number; ++i) {
                tiles_stack.Push (new TileInstance (tile));
            }
        }

        m_tilesA = Shuffle (m_tilesA);
        m_tilesB = Shuffle (m_tilesB);
        m_tilesC = Shuffle (m_tilesC);

        TrimDownTo (m_tilesA, 15 + (p_nb_players - 2) * 3);
        TrimDownTo (m_tilesB, 15 + (p_nb_players - 2) * 3);
        TrimDownTo (m_tilesC, 25 + (p_nb_players - 2) * 6);

        if (m_tilesC.Count < 5) {
            Debug.LogWarning ("Not enough tiles to add one more round tile!");
            return;
        }

        m_tilesC = InsertOneMoreRoundTile (m_tilesC, p_nb_players);
    }

    public List<Tile> LoadedTiles {
        get {
            return this.m_tiles_availables;
        }
    }

    public TileInstance PopNextTile ()
    {
        if (m_tilesA.Count == 0) {
            if (m_tilesB.Count == 0) {
                if (m_tilesC.Count == 0)
                    return null;

                TileInstance tile = m_tilesC.Pop ();

                if (tile is TileInstanceOneMoreRound) {
                    // Last Turn starting !
                    Suburbia.Bus.FireEvent (new EventLastTurn ());
                    // We put the next tile in the Real Estate Market
                    return PopNextTile();
                }

                return tile;
            }
            return m_tilesB.Pop ();
        }
        return m_tilesA.Pop ();
    }

    private void TrimDownTo (Stack<TileInstance> p_stack, int p_nb)
    {
        while (p_stack.Count > p_nb) {
            p_stack.Pop ();
        }
    }

    private Stack<TileInstance> Shuffle (Stack<TileInstance>  p_stack)
    {
        List<TileInstance> list = new List<TileInstance> (p_stack);

        System.Random rng = new System.Random ();
        int n = list.Count;

        while (n > 1) {
            n--;
            int k = rng.Next (n + 1);
            TileInstance tile = list [k];
            list [k] = list [n];
            list [n] = tile;
        }

        return new Stack<TileInstance> (list);
    }

    private Stack<TileInstance> InsertOneMoreRoundTile (Stack<TileInstance> p_stack, int p_nb_players)
    {
        TileInstance last = new TileInstanceOneMoreRound ();

        List<TileInstance> list = new List<TileInstance> (p_stack);

        System.Random rng = new System.Random ();

        int n = list.Count;
        int k = rng.Next (n - 10 - (p_nb_players - 2) * 3, n - 4);

        list.Insert (k, last);

        return new Stack<TileInstance> (list);
    }
}


