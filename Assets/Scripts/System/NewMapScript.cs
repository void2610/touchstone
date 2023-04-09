namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class NewMapScript : MonoBehaviour
	{
		public TileBase ground;

		public TileBase underground;

		public TileBase goal;

		int gHeight = 0;

		int maxRoomNum = 15;

		float enemyProbability = 0.7f;

		private Room[] rooms = new Room[100];

		private Area[] areas = new Area[100];

		private int minAreaSize = 5;

		public GameObject slime;

		private Tilemap tilemap;

		void SplitRecursion(Area a)
		{
			bool ok = true;
			if (Random.Range(0, 2) > 0.5f)
			{
				ok = a.SplitAreaX(Random.Range(minAreaSize, a.width));
				Debug.Log("Split X");
			}
			else
			{
				ok = a.SplitAreaY(Random.Range(minAreaSize, a.height));
				Debug.Log("Split Y");
			}
			if (ok)
			{
				SplitRecursion(a.children[0]);
				SplitRecursion(a.children[1]);
			}
		}

		void Start()
		{
			for (int i = 0; i < rooms.Length; i++)
			{
				rooms[i] = new Room();
				areas[i] = new Area();
			}

			tilemap = GetComponent<Tilemap>();

			Vector3 pos = new Vector3(0, 0, 0);

			areas[0] = new Area(new Vector3Int(0, 0, 0), 100, 100, minAreaSize);
			areas[0].tile1 = ground;
			areas[0].tile2 = underground;

			SplitRecursion(areas[0]);

		}
	}
}
