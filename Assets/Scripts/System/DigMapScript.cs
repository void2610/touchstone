using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigMapScript : MonoBehaviour
{

	[SerializeField]
	private TileBase ground;
	private Tilemap tilemap;
	private int width = 110;
	private int height = 110;
	private int[,] map;
	private int[] currentPos = { 0, 0 };
	private int[] lastPos = { 0, 0 };
	private int direction = 0; //0 = up, 1 = upright, 2 = right, 3 = downright, 4 = down, 5 = downleft, 6 = left, 7 = upleft
	private int radius = 3;
	private int count = 0;


	private int[,] GenerateArray(int width, int height, bool empty)
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

	private void DigTilemap(int x, int y, int radius)
	{
		for (int i = x - radius; i < x + radius; i++)
		{
			for (int j = y - radius; j < y + radius; j++)
			{
				if (i >= 0 && i < width && j >= 0 && j < height)
				{
					if (Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2) < Mathf.Pow(radius, 2))
					{
						map[i, j] = 0;
					}
				}
			}
		}
		count++;
	}

	private void ChangePosition(int direction)
	{
		switch (direction)
		{
			case 0:
			if (currentPos[1] + 1 < height)
			{
				currentPos[1]++;
			}
			break;
			case 1:
			if (currentPos[0] + 1 < width && currentPos[1] + 1 < height)
			{
				currentPos[0]++;
				currentPos[1]++;
			}
			break;
			case 2:
			if (currentPos[0] + 1 < width)
			{
				currentPos[0]++;
			}
			break;
			case 3:
			if (currentPos[0] + 1 < width && currentPos[1] - 1 > 0)
			{
				currentPos[0]++;
				currentPos[1]--;
			}
			break;
			case 4:
			if (currentPos[1] - 1 > 0)
			{
				currentPos[1]--;
			}
			break;
			case 5:
			if (currentPos[0] - 1 > 0 && currentPos[1] - 1 > 0)
			{
				currentPos[0]--;
				currentPos[1]--;
			}
			break;
			case 6:
			if (currentPos[0] - 1 > 0)
			{
				currentPos[0]--;
			}
			break;
			case 7:
			if (currentPos[0] - 1 > 0 && currentPos[1] + 1 < height)
			{
				currentPos[0]--;
				currentPos[1]++;
			}
			break;
		}
	}

	private IEnumerator Wait(float time)
	{

		yield return new WaitForSeconds(time);
	}

	private void ChangeRadius(int maxRadius, int minRadius)
	{
		if (Random.value < 0.3f)
		{
			if (Random.value < 0.5f)
			{
				if (radius < maxRadius)
				{
					radius++;
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

	private void DigTunnel(int startXpos, int startYpos, int maxRadius, int minRadius, int startDirection)
	{
		currentPos[0] = startXpos;
		currentPos[1] = startYpos;
		while (currentPos[0] > 0 && currentPos[0] < width && currentPos[1] > 0 && currentPos[1] < height)
		{
			DigTilemap(currentPos[0], currentPos[1], radius);
			RenderMap(map, tilemap, ground);

			ChangeRadius(maxRadius, minRadius);

			ChangeDirection();
			while (direction > startDirection + 4 || direction < startDirection - 4)
			{
				ChangeDirection();
			}

			ChangePosition(direction);
			Wait(0.1f);
		}
	}

	void Start()
	{

		tilemap = this.GetComponent<Tilemap>();
		map = GenerateArray(width, height, false);
		DigTunnel(5, 5, 15, 6, 2);
	}

	// Update is called once per frame
	void Update()
	{



	}
}
