namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class CreateMapScript : MonoBehaviour
	{
		public TileBase ground;

		public TileBase underground;

		public TileBase goal;

		int gHeight = 0;

		int maxRoomNum = 15;

		float enemyProbability = 0.7f;

		private Room[] rooms = new Room[100];

		public GameObject slime;

		private Tilemap tilemap;

		void Start()
		{
			for (int i = 0; i < rooms.Length; i++)
			{
				rooms[i] = new Room();
			}

			tilemap = GetComponent<Tilemap>();

			Vector3 pos = new Vector3(0, 0, 0);

			//ランダムな横幅の部屋を生成
			for (int i = 1; i < maxRoomNum; i++)
			{
				int len = (int)Random.Range(4, 10);
				int xPos = rooms[i - 1].position.x + rooms[i - 1].length;
				int yPos = 1; //(int)Random.Range(-1, 1);
				pos = new Vector3(xPos, yPos, 0);
				rooms[i] = new SurfaceRoom(tilemap, pos, len, ground, underground);

				//敵を生成
				if (Random.value < enemyProbability)
				{
					//Instantiate(slime, new Vector3Int(xPos + 1, yPos + 2, 0), Quaternion.identity);
				}
			}
			pos = new Vector3(rooms[maxRoomNum - 1].position.x + rooms[maxRoomNum - 1].length, pos.y, 0);
			rooms[maxRoomNum] = new GoalRoom(tilemap, pos, 10, goal, underground);
		}

		void Update()
		{
		}
	}
}
