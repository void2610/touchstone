namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class Room : MonoBehaviour
	{
		public Vector3Int position = new Vector3Int(0, 0, 0);
		public int width;
		public int height;
		public TileBase surface;
		//constructor
		public Room()
		{
			position = new Vector3Int(0, 0, 0);
			width = 0;
			height = 0;
			surface = null;
		}

		public Room(Vector3 pos, int wid, int hei, TileBase sur)
		{
			//Vector3Intに変換してpositionに代入
			position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			width = wid;
			height = hei;
			surface = sur;
		}

		public virtual void CreateRoomObject()
		{
		}

		public virtual void Start()
		{
		}

		public void CreateSurface()
		{
			for (int i = 0; i < width; i++)
			{
				GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, 0, 0), surface);
			}
		}

		public void CreateUnderGround()
		{
			TileBase underground = Resources.Load("Tiles/UndergroundTile") as TileBase;
			for (int i = 0; i < width; i++)
			{
				for (int k = -1; k > -20; k--)
				{
					GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, k, 0), underground);
				}
			}
		}
	}
}
