// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;
using System.Collections.Generic;

public class TileView : MonoBehaviour
{
    private static UnityEngine.Object m_tile_prefab = Resources.Load("Prefabs/Tile");
    private static Dictionary<ETileColor, Texture> m_textures = new Dictionary<ETileColor, Texture>();

    private TileInstance m_tile;

    private TileView()
    {
    }

    public static void InitTextures()
    {
        m_textures.Add(ETileColor.BLUE, Resources.Load("Textures/tile_top_blue.png") as Texture);
        m_textures.Add(ETileColor.YELLOW, Resources.Load("Textures/tile_top_yellow.png") as Texture);
        m_textures.Add(ETileColor.GREEN, Resources.Load("Textures/tile_top_green.png") as Texture);
        m_textures.Add(ETileColor.GREY, Resources.Load("Textures/tile_top_blue.png") as Texture);
        m_textures.Add(ETileColor.LAKE, Resources.Load("Textures/tile_top_blue.png") as Texture);
        m_textures.Add(ETileColor.NULL, null);
    }

    public static TileView Instantiate(TileInstance p_instance, Vector3 p_position)
    {
        // Creation of the new instance.
        GameObject _new_instance = UnityEngine.Object.Instantiate(m_tile_prefab) as GameObject;

        // Set tthe appropriate texture.
        _new_instance.renderer.material.SetTexture("_PathTex", m_textures[p_instance.color]);
        _new_instance.renderer.material.SetTexture("_PathMask", m_textures[p_instance.color]);

        // Get the script associated with the new tile.
        TileView _this = _new_instance.GetComponent<TileView>();
        _this.m_tile = p_instance;
        _new_instance.transform.position = p_position;

        return _this;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
