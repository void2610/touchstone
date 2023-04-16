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
		private int areaNum = 5;
		private float enemyProbability = 0.7f;
		private Room[] rooms = new Room[100];
		private Area[] areas = new Area[500];
		private int minAreaSize = 25;
		private int maxAreaSize = 32;
		private Tilemap tilemap;

		void CreateArea()
		{
			int x = Random.Range(minAreaSize + 10, maxAreaSize);
			int y = Random.Range(minAreaSize - 15, maxAreaSize - 15);
			areas[0] = new Area(new Vector3Int(0, 0, 0), x, y);
			for (int i = 1; i < areaNum; i++)
			{
				x = Random.Range(minAreaSize + 10, maxAreaSize);
				y = Random.Range(minAreaSize - 10, maxAreaSize);
				areas[i] = new Area(new Vector3Int(areas[i - 1].position.x + areas[i - 1].width, areas[i - 1].position.y + Random.Range(-3, 3), 0), x, y);
			}
		}

		void CreateRoom()
		{
			for (int i = 0; i < areaNum; i++)
			{
				rooms[i] = new Room(new Vector3Int(areas[i].position.x, areas[i].position.y, 0), areas[i].width, areas[i].height, ground);
				rooms[i].SetAllTileRoom(ground);
			}
		}

		void CreateMap()
		{
			CreateArea();
			CreateRoom();

			rooms[areaNum + 1] = new GoalRoom(new Vector3Int(areas[areaNum - 1].position.x + areas[areaNum - 1].width, areas[areaNum - 1].position.y + Random.Range(-3, 3), 0), 5, 5, ground);
		}


		void Start()
		{
			areaNum = Random.Range(5, 10);

			for (int i = 0; i < rooms.Length; i++)
			{
				rooms[i] = new Room();
				areas[i] = new Area();
			}

			CreateMap();
		}

	}
}
