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

		public static int[,] GenerateCellularAutomata(int width, int height, float seed, int fillPercent, bool edgesAreWalls)
		{
			// 乱数生成器にシード値を設定する
			System.Random rand = new System.Random(seed.GetHashCode());

			// マップを初期化する
			int[,] map = new int[width, height];

			for (int x = 0; x < map.GetUpperBound(0); x++)
			{
				for (int y = 0; y < map.GetUpperBound(1); y++)
				{
					// エッジが壁に設定されている場合は、セルが on（1）に設定されるようにする
					if (edgesAreWalls && (x == 0 || x == map.GetUpperBound(0) - 1 || y == 0 || y == map.GetUpperBound(1) - 1))
					{
						map[x, y] = 1;
					}
					else
					{
						// グリッドをランダムに生成する
						map[x, y] = (rand.Next(0, 100) < fillPercent) ? 1 : 0;
					}
				}
			}
			return map;
		}

		static int GetMooreSurroundingTiles(int[,] map, int x, int y, bool edgesAreWalls)
		{
			/* ムーア近傍は次のようになっている（「T」はタイル、「N」は近傍）。
			 *
			 * N N N
			 * N T N
			 * N N N
			 *
			 */

			int tileCount = 0;

			for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
			{
				for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
				{
					if (neighbourX >= 0 && neighbourX < map.GetUpperBound(0) && neighbourY >= 0 && neighbourY < map.GetUpperBound(1))
					{
						// 周囲のチェックが行われている、中心のタイルはカウントしない
						if (neighbourX != x || neighbourY != y)
						{
							tileCount += map[neighbourX, neighbourY];
						}
					}
				}
			}
			return tileCount;
		}

		public static int[,] SmoothMooreCellularAutomata(int[,] map, bool edgesAreWalls, int smoothCount)
		{
			for (int i = 0; i < smoothCount; i++)
			{
				for (int x = 0; x < map.GetUpperBound(0); x++)
				{
					for (int y = 0; y < map.GetUpperBound(1); y++)
					{
						int surroundingTiles = GetMooreSurroundingTiles(map, x, y, edgesAreWalls); if (edgesAreWalls && (x == 0 || x == (map.GetUpperBound(0) - 1) || y == 0 || y == (map.GetUpperBound(1) - 1)))
						{ // edgesAreWalls が true であればエッジを壁として設定する
							map[x, y] = 1;
						} // デフォルトのムーア近傍の規則では 5 つ以上の近傍が必要
						else if (surroundingTiles > 4)
						{
							map[x, y] = 1;
						}
						else if (surroundingTiles < 4)
						{
							map[x, y] = 0;
						}
					}
				}
			}
			// 戻り値として修正されたマップを返す
			return map;
		}

		void Start()
		{
			tilemap = this.GetComponent<Tilemap>();

			//マップの幅と高さを設定する
			int width = 100;
			int height = 100;
			//現実の時間をシード値として使用する
			float seed = Time.realtimeSinceStartup;
			//セルオートマトンでマップを生成する
			int[,] map = GenerateCellularAutomata(width, height, seed, 50, true);
			//マップを平滑化する
			map = SmoothMooreCellularAutomata(map, true, 5);

			//マップを描画する
			RenderMap(map, tilemap, ground);
		}

		void Update()
		{
		}
	}
}
