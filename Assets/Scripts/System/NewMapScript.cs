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
		private int maxRoomNum = 30;
		private int minRoomNum = 15;
		float enemyProbability = 0.7f;
		private Room[] rooms = new Room[100];
		private Area firstArea;
		private Area[] areas = new Area[500];
		private int minAreaSize = 10;
		public GameObject slime;
		private Tilemap tilemap;
		private int areaCount = 0;
		private int loopCount = 0;
		private bool loopFlag = true;

		void SplitAreaRecursion(Area a)
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
				SplitAreaRecursion(a.children[0]);
				SplitAreaRecursion(a.children[1]);
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

		Room CreateRoom(Area a, int minSize)
		{
			//ランダムな大きさのRoomをAreaの内部に生成し、返す
			Vector3Int pos = new Vector3Int(Random.Range(a.position.x + 1, a.position.x + a.width - 1), Random.Range(a.position.y + 1, a.position.y + a.height - 1), 0);
			int wid = Random.Range(minSize, a.width - 1);
			int hei = Random.Range(minSize, a.height - 1);
			Room r = new Room(pos, wid, hei, ground, underground);
			return r;
		}
		void CreateMap()
		{
			tilemap.ClearAllTiles();

			firstArea = new Area(new Vector3Int(0, 0, 0), 75, 75, minAreaSize);
			firstArea.tile1 = ground;
			firstArea.tile2 = underground;

			SplitAreaRecursion(firstArea);

			areaCount = 0;
			SearchAllArea(firstArea);

			for (int i = 0; i < areaCount; i++)
			{
				rooms[i] = CreateRoom(areas[i], 3);
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

			firstArea = new Area(new Vector3Int(0, 0, 0), 75, 75, minAreaSize);
			firstArea.tile1 = ground;
			firstArea.tile2 = underground;


			//いい感じになるまでループ
			while (areaCount > maxRoomNum || areaCount < minRoomNum || loopFlag)
			{
				CreateMap();

				//ループ回数が多すぎたらやめる
				if (loopCount > 100)
				{
					Debug.Log("ループ回数が多すぎます");
					break;
				}

				//大きすぎる部屋があったらやり直し
				loopFlag = false;
				for (int i = 0; i < areaCount; i++)
				{
					if (areas[i].GetSize() > 1000)
					{
						loopFlag = true;
					}
				}
				loopCount++;
			}
		}

	}
}
