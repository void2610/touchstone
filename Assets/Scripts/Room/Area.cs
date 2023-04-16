namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class Area : MonoBehaviour
	{
		public Vector3Int position = new Vector3Int(0, 0, 0);
		public int width;
		public int height;
		public Area[] children;
		public TileBase tile1;
		public TileBase tile2;

		//constructor
		public Area()
		{
			position = new Vector3Int(-1, -1, -1);
			width = 0;
			height = 0;
			children = new Area[2];
		}

		public Area(Vector3 pos, int wid, int hei)
		{
			//Vector3Intに変換してpositionに代入
			position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			width = wid;
			height = hei;
			children = new Area[2];
		}

		public void SetAllTile(TileBase tile)
		{
			for (int i = 0; i < width; i++)
			{
				for (int k = 0; k < height; k++)
				{
					//GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, k, 0), tile);
					//エリアの外枠だけにTileをセット
					if (i == 0 || k == 0 || i == width - 1 || k == height - 1)
					{
						GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(position + new Vector3Int(i, k, 0), tile);
					}
				}
			}
		}

		public int GetSize()
		{
			return width * height;
		}
	}
}
