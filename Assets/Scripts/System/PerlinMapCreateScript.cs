namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class PerlinMapCreateScript : MonoBehaviour
	{
		[SerializeField]
		private TileBase ground;
		private Tilemap tilemap;

		public static int[,] GenerateArray(int width, int height, bool empty)
		{
			int[,] map = new int[width, height];
			for (int x = 0; x < map.GetUpperBound(0); x++)
			{
				for (int y = 0; y < map.GetUpperBound(1); y++)
				{
					if (empty)
					{
						map[x, y] = 0;
					}
					else
					{
						map[x, y] = 1;
					}
				}
			}
			return map;
		}

		public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
		{
			//マップをクリアする（重複しないようにする）
			tilemap.ClearAllTiles();
			//マップの幅の分、周回する
			for (int x = 0; x < map.GetUpperBound(0); x++)
			{
				//マップの高さの分、周回する
				for (int y = 0; y < map.GetUpperBound(1); y++)
				{
					// 1 = タイルあり、0 = タイルなし
					if (map[x, y] == 1)
					{
						tilemap.SetTile(new Vector3Int(x, y, 0), tile);
					}
				}
			}
		}

		public static void UpdateMap(int[,] map, Tilemap tilemap) //マップとタイルマップを取得し、null タイルを必要箇所に設定する
		{
			for (int x = 0; x < map.GetUpperBound(0); x++)
			{
				for (int y = 0; y < map.GetUpperBound(1); y++)
				{
					//再レンダリングではなく、マップの更新のみを行う
					//これは、それぞれのタイル（および衝突データ）を再描画するのに比べて
					//タイルを null に更新するほうが使用リソースが少なくて済むためです。
					if (map[x, y] == 0)
					{
						tilemap.SetTile(new Vector3Int(x, y, 0), null);
					}
				}
			}
		}

		public static int[,] PerlinNoise(int[,] map, float seed)
		{
			int newPoint;
			//パーリンノイズのポイントの位置を下げるために使用される
			float reduction = 0.5f;
			//パーリンノイズを生成する
			for (int x = 0; x < map.GetUpperBound(0); x++)
			{
				newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * map.GetUpperBound(1));

				//高さの半分の位置付近からノイズが始まるようにする
				newPoint += (map.GetUpperBound(1) / 2);
				for (int y = newPoint; y >= 0; y--)
				{
					map[x, y] = 1;
				}
			}
			return map;
		}

		void Start()
		{
			tilemap = this.GetComponent<Tilemap>();

			//マップの幅と高さを設定する
			int width = 100;
			int height = 100;
			//マップを生成する
			int[,] map = GenerateArray(width, height, true);
			//マップをパーリンノイズで生成する
			map = PerlinNoise(map, 0.13f);
			//マップを更新する
			RenderMap(map, tilemap, ground);
		}

		void Update()
		{
		}
	}
}