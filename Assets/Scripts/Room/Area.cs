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

		public int minSize = 1;

		public Area[] children;

		public TileBase tile1;

		public TileBase tile2;

		//constructor
		public Area()
		{
			position = new Vector3Int(0, 0, 0);
			width = 0;
			height = 0;
			children = new Area[2];
		}

		public Area(Vector3 pos, int wid, int hei, int min)
		{
			//Vector3Intに変換してpositionに代入
			position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			width = wid;
			height = hei;
			minSize = min;
			children = new Area[2];
		}

		public bool SplitAreaX(int x)
		{
			if (x < minSize || width - x < minSize)
			{
				return false;
			}
			Area area1 = new Area(this.position, x, this.height, this.minSize);
			Area area2 = new Area(new Vector3Int(this.position.x + x, this.position.y, this.position.z), this.width - x, this.height, this.minSize);
			children[0] = area1;
			children[0].tile1 = tile1;
			children[0].tile2 = tile2;
			children[1] = area2;
			children[1].tile1 = tile1;
			children[1].tile2 = tile2;

			children[0].SetAllTile(tile1);
			children[1].SetAllTile(tile2);
			return true;
		}

		public bool SplitAreaY(int y)
		{
			if (y < minSize || height - y < minSize)
			{
				return false;
			}
			Area area1 = new Area(this.position, this.width, y, this.minSize);
			Area area2 = new Area(new Vector3Int(this.position.x, this.position.y + y, this.position.z), this.width, this.height - y, this.minSize);
			children[0] = area1;
			children[0].tile1 = tile1;
			children[0].tile2 = tile2;
			children[1] = area2;
			children[1].tile1 = tile1;
			children[1].tile2 = tile2;

			children[0].SetAllTile(tile1);
			children[1].SetAllTile(tile2);
			return true;
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
	}
}
