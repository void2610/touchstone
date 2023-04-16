namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class SurfaceRoom : Room
	{
		public Tilemap tilemap;

		//constructor
		public SurfaceRoom() : base()
		{
		}

		public SurfaceRoom(Tilemap tm, Vector3 pos, int wid, int hei, TileBase sur, TileBase und) : base(pos, wid, hei, sur, und)
		{
			tilemap = tm;
			base.position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			base.width = wid;
			base.height = hei;
			base.surface = sur;
			base.underground = und;
			CreateFloor();
		}

		public override void CreateFloor()
		{
			for (int i = 0; i < width; i++)
			{
				tilemap.SetTile(position + new Vector3Int(i, 0, 0), surface);
				for (int k = -1; k > -20; k--)
				{
					tilemap.SetTile(position + new Vector3Int(i, k, 0), underground);
				}
			}
		}
	}
}
