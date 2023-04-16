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
		public int minSize = 1;
		public TileBase surface;
		public TileBase underground;
		//constructor
		public Room()
		{
			position = new Vector3Int(0, 0, 0);
			width = 0;
			height = 0;
			minSize = 1;
			surface = null;
			underground = null;
		}

		public Room(Vector3 pos, int wid, int hei, TileBase sur, TileBase und)
		{
			//Vector3Intに変換してpositionに代入
			position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			width = wid;
			height = hei;
			surface = sur;
			underground = und;
		}

		public virtual void CreateFloor()
		{
		}

		public virtual void Start()
		{
		}

		public void SetAllTileRoom(TileBase tile)
		{
			for (int i = 0; i < width; i++)
			{
				for (int k = 0; k < height; k++)
				{
					//GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, k, 0), tile);
					//エリアの外枠だけにTileをセット
					if (k == 0)
					{
						GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, k, 0), tile);
					}
				}
			}
		}
	}
}
