namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class GoalRoom : SurfaceRoom
	{
		//constructor
		public GoalRoom() :
				base()
		{
		}

		public GoalRoom(Vector3 pos, int wid, int hei, TileBase sur) : base(pos, wid, hei, sur)
		{
			base.position = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
			base.width = wid;
			base.height = hei;
			base.surface = sur;
			CreateRoomObject();
		}

		private GameObject GoalArea;

		public override void CreateRoomObject()
		{
			CreateGoal();
		}

		void CreateGoal()
		{
			//ゴールエリアをPrefabフォルダから読み込む
			GoalArea = (GameObject)Resources.Load("Prefabs/Room/GoalArea");

			//ゴールエリアをpositionの位置に生成
			Vector3 goalPosition = new Vector3(position.x + width / 2, position.y + 2, 0);
			GameObject _goal = Instantiate(GoalArea, goalPosition, Quaternion.identity);

			//ゴールエリアをlengthの長さに変形
			_goal.transform.localScale = new Vector3(width, 5, 1);
		}
	}
}
