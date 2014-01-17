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
    private static UnityEngine.Object m_tile_prefab = Resources.Load ("Prefabs/Tile");
    private static Dictionary<ETileColor, Texture> m_textures = new Dictionary<ETileColor, Texture> ();
    private TileInstance m_tile;

    private TileView ()
    {
    }

    public static void InitTextures ()
    {
        m_textures.Add (ETileColor.BLUE, Resources.Load<Texture> ("Textures/tile_top_blue"));
        m_textures.Add (ETileColor.YELLOW, Resources.Load<Texture> ("Textures/tile_top_yellow") );
        m_textures.Add (ETileColor.GREEN, Resources.Load<Texture> ("Textures/tile_top_green") );
        m_textures.Add (ETileColor.GREY, Resources.Load<Texture>("Textures/tile_top_blue") );
        m_textures.Add (ETileColor.LAKE, Resources.Load<Texture> ("Textures/tile_top_blue") );
        m_textures.Add (ETileColor.NULL, null);
    }

    public static TileView Instantiate (TileInstance p_instance, Vector3 p_position, float p_scale)
    {
        try {
            // Creation of the new instance.
            GameObject _new_instance = UnityEngine.Object.Instantiate (m_tile_prefab) as GameObject;
            _new_instance.transform.position = p_position;
            _new_instance.transform.localScale = new Vector3(p_scale, p_scale, p_scale);
            _new_instance.transform.localRotation = Quaternion.Euler(0,180,-30);
            _new_instance.layer = 8;

            // Set tthe appropriate texture.
            _new_instance.renderer.material.SetTexture ("_PathTex", m_textures [p_instance.color]);
            _new_instance.renderer.material.SetTexture ("_PathMask", m_textures [p_instance.color]);

            // Get the script associated with the new tile.
            TileView _this = _new_instance.GetComponent<TileView> ();
            _this.m_tile = p_instance;

            // Set an other script to this instance linked
            _new_instance.AddComponent<SmoothTranslationMarket>().InitWith(p_position + new Vector3(0, 0.5f, 0));

            Debug.Log("Tile : " + p_instance.name + " loaded to market place.");

            return _this;
        } catch (Exception e) {
            Debug.LogError ("Error while instanciating tile !");
            return null;
        }
    }

    void Start ()
    {
    }

    void Update ()
    {
    }

    void OnMouseEnter()
    {
        Suburbia.Bus.fireEvent(new EventShowTileInformation (true, this.m_tile.description));
    }

    void OnMouseExit()
    {
        Suburbia.Bus.fireEvent(new EventShowTileInformation (false, this.m_tile.description));
    }

    void OnMouseDown()
    {

    }
}
