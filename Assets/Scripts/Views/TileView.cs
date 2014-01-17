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
    private static float m_offset_x;
    private static float m_offset_y;

    private TileInstance m_tile;

    private TileView ()
    {
    }

    public static void InitProperties ()
    {
        m_textures.Add (ETileColor.BLUE, Resources.Load<Texture> ("Textures/tile_top_blue"));
        m_textures.Add (ETileColor.YELLOW, Resources.Load<Texture> ("Textures/tile_top_yellow"));
        m_textures.Add (ETileColor.GREEN, Resources.Load<Texture> ("Textures/tile_top_green"));
        m_textures.Add (ETileColor.GREY, Resources.Load<Texture> ("Textures/tile_top_blue"));
        m_textures.Add (ETileColor.LAKE, Resources.Load<Texture> ("Textures/tile_top_blue"));
        m_textures.Add (ETileColor.NULL, null);

        GameObject tile = Instantiate(Resources.Load("Prefabs/Tile")) as GameObject;
        m_offset_x = tile.GetComponent<SphereCollider>().bounds.size.x;
        m_offset_y = 1.80f;
        Destroy(tile);
    }

    public static TileView InstantiateWithParent (TileInstance p_instance, Transform p_parent)
    {
        try {
            // Creation of the new instance.
            GameObject _new_instance = UnityEngine.Object.Instantiate (m_tile_prefab) as GameObject;
            _new_instance.transform.parent = p_parent;

            // Get the script associated with the new tile.
            TileView = _new_instance.AddComponent<TileView> ();
            _this.m_tile = p_instance;

            // Set the common properties
            SetTileProperties(_new_instance, p_instance);

            return _this;

        } catch (Exception) {
            Debug.LogError ("Error while instantiating !");
            return null;
        }
    }

    public static TileViewREM InstantiateForRealEstateMarket (TileInstance p_instance, Vector3 p_position, float p_scale)
    {
        try {
            // Creation of the new instance.
            GameObject _new_instance = UnityEngine.Object.Instantiate (m_tile_prefab) as GameObject;

            _new_instance.transform.position = p_position;
            _new_instance.transform.localScale = new Vector3 (p_scale, p_scale, p_scale);
            _new_instance.transform.localRotation = Quaternion.Euler (0, 180, -30);
            _new_instance.layer = 8;

            // Get the script associated with the new tile.
            TileREMView = _new_instance.AddComponent<TileREMView> ();
            _this.m_tile = p_instance;

            // Set the common properties
            SetTileProperties(_new_instance, _this);

            // Set an other script to this instance linked
            _new_instance.AddComponent<SmoothTranslationMarket> ().InitWith (p_position + new Vector3 (0, 0.5f, 0));

            Debug.Log ("Tile : " + p_instance.name + " loaded to market place.");

            return _this;
        } catch (Exception e) {
            Debug.LogError ("Error while instanciating tile !");
            return null;
        }
    }

    private static void SetTileProperties(GameObject p_tile, TileView _this)
    {
        // Set tthe appropriate texture.
        p_tile.renderer.material.SetTexture ("_PathTex", m_textures [_this.m_tile.color]);
        p_tile.renderer.material.SetTexture ("_PathMask", m_textures [_this.m_tile.color]);

        // Set the initial position if one does exists.
        if(_this.m_tile.position != null)
        {
            p_tile.transform.position = new Vector3(_this.m_tile.position.x * m_offset_x, _this.m_tile.position.y * m_offset_y);
        }
    }

    void Start ()
    {
    }

    void Update ()
    {
    }
}
