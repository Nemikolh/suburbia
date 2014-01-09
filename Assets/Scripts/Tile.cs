using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Tile
{
		private readonly string m_name;
		private readonly ETileColor m_color;
		private readonly ETileIcon m_icon;
		private readonly int m_price;
		private readonly int m_number;
		private readonly ETileLetter m_letter;
		private readonly Effect m_immediate_effect;
		private readonly List<Trigger> m_triggers;

		public static Tile LoadFromJson (JObject p_json)
		{
				try {
						string name = Util.getValue<string> (p_json, "name");
						ETileColor color = Util.parseEnum<ETileColor> (Util.getValue<string> (p_json, "color"));
						ETileIcon icon = Util.parseEnum<ETileIcon> (Util.getValue<string> (p_json, "icon"));
						int price = Util.getValue<int> (p_json, "price");
						ETileLetter letter = Util.parseEnum<ETileLetter> (Util.getValue<string> (p_json, "letter"));
						int number = Util.getValue<int> (p_json, "number");
						List<Trigger> triggers = new List<Trigger> ();
						// TODO load triggers
						return new Tile (name, color, icon, price, letter, number, triggers);
						// TODO : load immediate Effect
				} catch (ArgumentException) {
						Debug.Log ("Error while loading Tile from Json !");
						return null;
				}
		}

		private Tile (string p_name, ETileColor p_color, ETileIcon p_icon, int p_price, ETileLetter p_letter, int p_number, List<Trigger> p_triggers)
		{
				m_name = p_name;
				m_color = p_color;
				m_icon = p_icon;
				m_price = p_price;
				m_letter = p_letter;
				m_number = p_number;
				m_triggers = p_triggers;
		}

		public bool IsOfType (TileType p_type)
		{
				if (p_type.IsColor ()) {
						return m_color == p_type.color;
				}

				if (p_type.IsIcon ()) {
						return m_icon == p_type.icon;
				}

				Debug.Log ("TileType is not an icon or a color !");
				return false;
		}

		public Trigger triggers {
				get {
						return this.m_triggers;
				}
		}
		
		public string name {
				get {
						return m_name;
				}
		}

		public ETileColor color {
				get {
						return m_color;
				}
		}

		public ETileIcon icon {
				get {
						return m_icon;
				}
		}

		public int price {
				get {
						return m_price;
				}
		}

		public int number {
				get {
						return this.m_number;
				}
		}

		public ETileLetter letter {
				get {
						return m_letter;
				}
		}

		public Effect effect {
				get {
						return this.m_immediate_effect;
				}
		}

		public List<TileType> types {
				get {
						List<TileType> list = new List<TileType> ();
						list.Add (new TileType (this.m_color));
						if (m_icon != ETileIcon.NONE)
								list.Add (new TileType (this.m_icon));
						return list;
				}
		}
}
