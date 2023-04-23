using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigMapScript : MonoBehaviour
{

	[SerializeField]
	private TileBase ground;
	private Tilemap tilemap;
	private int width = 250;
	private int height = 250;
	public int[,] map = new int[500, 500];
	private int[] currentPos = { 1, 1 };
	private int direction = 1; //0 = up, 1 = upright, 2 = right, 3 = downright, 4 = down, 5 = downleft, 6 = left, 7 = upleft
	private int radius;
	private int count = 0;
	private int seed = -1;
	private int margin = 10;
	private int countLimit = 100;
	private int startDirection = 1;

	private int[,] GenerateArray(int width, int height, bool empty)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
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

	private void DigMap(int x, int y, int radius)
	{
		for (int i = x - radius; i < x + radius; i++)
		{
			for (int j = y - radius; j < y + radius; j++)
			{
				if (Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2) < Mathf.Pow(radius, 2))
				{
					if (i >= 0 && j >= 0 && i < width && j < height)
					{
						map[i, j] = 0;
					}
				}
			}
		}
	}

	private void ChangePosition(int direction)
	{
		switch (direction)
		{
			case 0:
			currentPos[1]++;
			break;
			case 1:
			currentPos[0]++;
			currentPos[1]++;
			break;
			case 2:
			currentPos[0]++;
			break;
			case 3:
			currentPos[0]++;
			currentPos[1]--;
			break;
			case 4:
			currentPos[1]--;
			break;
			case 5:
			currentPos[0]--;
			currentPos[1]--;
			break;
			case 6:
			currentPos[0]--;
			break;
			case 7:
			currentPos[0]--;
			currentPos[1]++;
			break;
		}
	}
	private void ChangeRadius(int maxRadius, int minRadius)
	{
		if (Random.Range(0, 100) < 30)
		{

			if (Random.Range(0, 100) < 50)
			{
				if (radius < maxRadius)
				{
					radius++;
				}
			}
		}
		else
		{
			if (radius > minRadius)
			{
				radius--;
			}
		}
	}
	private void ChangeDirection()
	{
		if (Random.value < 0.7f)
		{
			if (Random.value < 0.5f)
			{
				if (direction < 7)
				{
					direction++;
				}
			}
			else
			{
				if (direction > 0)
				{
					direction--;
				}
			}
		}
	}

	private bool CheckNextPosition()
	{
		if (currentPos[0] - radius < 0 || currentPos[1] - radius < 0)
		{
			return false;
		}
		switch (direction)
		{
			case 0:
			if (map[currentPos[0], currentPos[1] + radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 1:
			if (map[currentPos[0] + radius, currentPos[1] + radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 2:
			if (map[currentPos[0] + radius, currentPos[1]] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 3:
			if (map[currentPos[0] + radius, currentPos[1] - radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 4:
			if (map[currentPos[0], currentPos[1] - radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 5:
			if (map[currentPos[0] - radius, currentPos[1] - radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 6:
			if (map[currentPos[0] - radius, currentPos[1]] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			case 7:
			if (map[currentPos[0] - radius, currentPos[1] + radius] == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
			default:
			return false;
		}
	}

	private bool CheckArea(int x, int y, int radius)
	{
		for (int i = x - radius; i < x + radius; i++)
		{
			for (int j = y - radius; j < y + radius; j++)
			{
				if (i < 0 || i >= width || j < 0 || j >= height)
				{
					continue;
				}
				if (map[i, j] == 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void DigTunnel(int startXpos, int startYpos, int maxRadius, int minRadius, int startDirection)
	{
		currentPos[0] = startXpos;
		currentPos[1] = startYpos;
		direction = startDirection;
		radius = Random.Range(minRadius, maxRadius);
		while (currentPos[0] - radius > 0 && currentPos[0] + radius < width && currentPos[1] - radius > 0 && currentPos[1] + radius < height && CheckNextPosition())
		{
			DigMap(currentPos[0], currentPos[1], radius);


			ChangeRadius(maxRadius, minRadius);

			//斜めにずれすぎないようにする
			ChangeDirection();
			while (direction > startDirection + 1 || direction < startDirection - 1)
			{
				ChangeDirection();
			}

			ChangePosition(direction);
		}
		DigMap(currentPos[0], currentPos[1], radius + 2);
	}

	private int CountMapChange(int[,] oldMap, int[,] newMap)
	{
		int res = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (oldMap[i, j] != newMap[i, j])
				{
					res++;
				}
			}
		}
		return res;
	}
	private int CountDigedArea(int[,] map)
	{
		int res = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (map[i, j] == 0)
				{
					res++;
				}
			}
		}
		return res;
	}
	private void ChangeStartDirectionToCenter()
	{
		if (currentPos[0] < width / 2 && currentPos[1] < height / 3)
		{
			startDirection = 1;
		}
		else if (currentPos[0] < width / 2 && currentPos[1] > height / 3 && currentPos[1] < height / 3 * 2)
		{
			startDirection = 2;
		}
		else if (currentPos[0] < width / 2 && currentPos[1] > height / 3 * 2)
		{
			startDirection = 3;
		}
		else if (currentPos[0] > width / 2 && currentPos[1] > height / 3 * 2)
		{
			startDirection = 5;
		}
		else if (currentPos[0] > width / 2 && currentPos[1] > height / 3 && currentPos[1] < height / 3 * 2)
		{
			startDirection = 6;
		}
		else if (currentPos[0] > width / 2 && currentPos[1] < height / 3)
		{
			startDirection = 7;
		}
	}
	private void DigTunnelUntillSuccess(int startXpos, int startYpos, int maxRadius, int minRadius, int minSize)
	{
		ChangeStartDirectionToCenter();
		count++;
		if (count > countLimit)
		{
			Debug.Log("failed");
			return;
		}
		int[,] oldMap = new int[500, 500];
		System.Array.Copy(map, oldMap, map.Length);
		DigTunnel(startXpos, startYpos, maxRadius, minRadius, startDirection);
		if (CountMapChange(oldMap, map) < minSize)
		{
			map = oldMap;

			while (!CheckArea(currentPos[0], currentPos[1], radius))
			{
				startXpos = Random.Range(margin, width - margin);
				currentPos[0] = startXpos;
				startYpos = Random.Range(margin, height - margin);
				currentPos[1] = startYpos;
			}

			ChangeStartDirectionToCenter();

			DigTunnelUntillSuccess(startXpos, startYpos, maxRadius, minRadius, minSize);
		}
		else
		{
			count = 0;
		}
	}


	void Start()
	{
		tilemap = this.GetComponent<Tilemap>();
		map = GenerateArray(width, height, false);
		seed = System.DateTime.Now.Millisecond;
		Random.InitState(seed);

		//メインの穴を1つ生成
		DigTunnelUntillSuccess(15, 15, 20, 9, 5000);


		//いい感じのところに移動
		currentPos[0] = Random.Range(margin, width - margin);
		currentPos[1] = Random.Range(margin, height - margin);

		while (CountDigedArea(map) < 15000 && count < countLimit)
		{
			//細い穴を複数生成
			while (!CheckArea(currentPos[0], currentPos[1], radius))
			{
				currentPos[0] = Random.Range(margin, width - margin);
				currentPos[1] = Random.Range(margin, height - margin);
			}
			int limit = 100;
			DigTunnelUntillSuccess(currentPos[0], currentPos[1], 5, 3, limit);
		}
		if (count > countLimit)
		{
			Debug.Log("failed");
		}

		RenderMap(map, tilemap, ground);
	}
}
