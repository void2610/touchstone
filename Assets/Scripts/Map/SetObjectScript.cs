using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectScript : MonoBehaviour
{
	private int[,] map = new int[500, 500];
	private GameObject goal;
	private int goalSize = 3;
	void Start()
	{
		map = GameObject.Find("Tilemap").GetComponent<DigMapScript>().map;
		goal = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Map/GoalArea"));

		//mapを探索して0が縦横goalSize以上続いているところにゴールを設置する
		for (int x = 0; x < map.GetUpperBound(0); x++)
		{
			for (int y = 0; y < map.GetUpperBound(1); y++)
			{
				if (map[x, y] == 0)
				{
					int count = 0;
					for (int i = 0; i < goalSize; i++)
					{
						if (map[x + i, y] == 1)
						{
							count++;
						}
					}
					for (int i = 0; i < goalSize; i++)
					{
						if (map[x, y + i] == 1)
						{
							count++;
						}
					}
					if (count == 0)
					{
						Instantiate(goal, new Vector3(x + goalSize / 2, y + goalSize / 2, 0), Quaternion.identity);
						Debug.Log("x:" + (x + goalSize / 2) + " y:" + (y + goalSize / 2));
						return;
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
