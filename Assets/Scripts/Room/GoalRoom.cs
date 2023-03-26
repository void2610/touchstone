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

        public GoalRoom(
            Tilemap tm,
            Vector3 pos,
            int len,
            TileBase sur,
            TileBase und
        ) :
            base(tm, pos, len, sur, und)
        {
            base.position =
                new Vector3Int((int) pos.x, (int) pos.y, (int) pos.z);
            base.length = len;
            base.surface = sur;
            base.underground = und;
            CreateFloor();
            CreateGoal();
        }

        private GameObject GoalArea;

        void CreateGoal()
        {
            //ゴールエリアをPrefabフォルダから読み込む
            GoalArea = (GameObject) Resources.Load("Prefabs/Room/GoalArea");

            //ゴールエリアをpositionの位置に生成
            Vector3 goalPosition =
                new Vector3(position.x + length / 2, position.y + 2, 0);
            GameObject _goal =
                Instantiate(GoalArea, goalPosition, Quaternion.identity);

            //ゴールエリアをlengthの長さに変形
            _goal.transform.localScale = new Vector3(length, 5, 1);
        }
    }
}
