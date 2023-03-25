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

        int gHeight = 0;

        int maxRoomNum = 1;

        public float enemyProbability = 0.3f;

        //private Room[] rooms;
        public GameObject slime;

        private Tilemap tilemap;

        void Start()
        {
            tilemap = GetComponent<Tilemap>();

            //ランダムな横幅の部屋を生成
            for (int i = 0; i < maxRoomNum; i++)
            {
                Vector3 pos = new Vector3(0, 0, 0);
                Room rooms =
                    new SurfaceRoom(tilemap,
                        pos,
                        (int) Random.Range(10, 15),
                        ground,
                        underground);
            }

            // for (int i = 0; i < maxRoomNum; i++)
            // {
            //     //地表と地下のTilemapを1部屋分配置
            //     for (int j = leftSide[i] - 1; j < rightSide[i]; j++)
            //     {
            //         tilemap.SetTile(new Vector3Int(j, gHeight, 0), ground);
            //         for (int k = gHeight - 1; k > gHeight - 20; k--)
            //         {
            //             tilemap.SetTile(new Vector3Int(j, k, 0), underground);
            //         }
            //     }

            //     //敵を生成
            //     if (Random.value < enemyProbability)
            //     {
            //         Instantiate(slime,
            //         new Vector3Int(rightSide[i] - 1, gHeight + 2, 0),
            //         Quaternion.identity);
            //     }

            //     //ランダムに高さを変更
            //     gHeight += Random.Range(-2, 2);
            //     if (gHeight > 4)
            //     {
            //         gHeight = 3;
            //     }
            //     else if (gHeight < -4)
            //     {
            //         gHeight = -3;
            //     }
            // }
        }

        void Update()
        {
        }
    }
}
