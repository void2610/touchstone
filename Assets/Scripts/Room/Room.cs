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

		public TileBase underground;

		//constructor
		public Room()
		{
			position = new Vector3Int(0, 0, 0);
			width = 0;
			surface = null;
			underground = null;
		}

		public Room(Vector3 pos, int wid, TileBase sur, TileBase und)
		{
			//Vector3Intに変換してpositionに代入
			position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			width = wid;
			surface = sur;
			underground = und;
		}

		public virtual void CreateFloor()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}
	}
}
