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
		private int maxAreaSize = 40;
		private Tilemap tilemap;
		private int loopCount = 0;
		private bool loopFlag = true;

		void CreateArea()
		{
			int x = Random.Range(minAreaSize + 10, maxAreaSize);
			int y = Random.Range(minAreaSize - 10, maxAreaSize);
			areas[0] = new Area(new Vector3Int(0, 0, 0), x, y);
			for (int i = 1; i < areaNum; i++)
			{
				x = Random.Range(minAreaSize + 10, maxAreaSize);
				y = Random.Range(minAreaSize - 10, maxAreaSize);
				areas[i] = new Area(new Vector3Int(areas[i - 1].position.x + areas[i - 1].width, areas[i - 1].position.y + Random.Range(-3, 3), 0), x, y);
				areas[i].SetAllTile(ground);
				Debug.Log(" width:" + areas[i].width + " height:" + areas[i].height);
			}
		}

		void CreateMap()
		{
			tilemap.ClearAllTiles();
			CreateArea();
		}


		void Start()
		{
			areaNum = Random.Range(5, 10);

			for (int i = 0; i < rooms.Length; i++)
			{
				rooms[i] = new Room();
				areas[i] = new Area();
			}

			tilemap = GetComponent<Tilemap>();
			CreateMap();

			Vector3 pos = new Vector3(0, 0, 0);
		}

	}
}
