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

		private Area firstArea;

		private Area[] areas = new Area[100];

		private int minAreaSize = 17;

		public GameObject slime;

		private Tilemap tilemap;

		private int areaCount = 0;

		void SplitRecursion(Area a)
		{
			bool ok = true;
			//50%の確率でX方向に分割、50%の確率でY方向に分割
			if (Random.value < 0.5f)
			{
				ok = a.SplitAreaX(Random.Range(minAreaSize, a.width));
			}
			else
			{
				ok = a.SplitAreaY(Random.Range(minAreaSize, a.height));
			}
			if (ok)
			{
				SplitRecursion(a.children[0]);
				SplitRecursion(a.children[1]);
			}
		}

		void SearchAllArea(Area root)
		{
			try
			{
				if (root.children[0].position != new Vector3Int(-1, -1, -1) && root.children[1].position != new Vector3Int(-1, -1, -1))
				{
					SearchAllArea(root.children[0]);
					SearchAllArea(root.children[1]);
				}
				else
				{
					areas[areaCount] = root;
					areaCount++;
					Debug.Log(areaCount);
				}
			}
			catch (System.Exception e)
			{
				areas[areaCount] = root;
				areaCount++;
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

			firstArea = new Area(new Vector3Int(0, 0, 0), 100, 100, minAreaSize);
			firstArea.tile1 = ground;
			firstArea.tile2 = underground;

			SplitRecursion(firstArea);
			SearchAllArea(firstArea);

			for (int i = 0; i < areaCount; i++)
			{
				Debug.Log(areas[i].position);
			}
		}

	}
}