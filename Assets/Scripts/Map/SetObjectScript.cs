using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectScript : MonoBehaviour
{
	private DigMapScript digMapScript;
	private int[,] map = new int[500, 500];
	private GameObject goal;
	private int goalSize = 4;
	private int goalDistance = 50;
	private bool isGoalCreated = false;
	private GameObject player;
	private int startPosition = 1;
	private int mapSize = 245;

	private bool CheckSpace(int x, int y, int size)
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

				Vector3 pos = new Vector3(x + goalSize / 2, y + goalSize / 2, 0);
				if (Vector3.Distance(pos, player.transform.position) > goalDistance)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		return false;
	}

	private Vector3 ReturnGroundPosition(Vector3 pos)
	{
		//mapを確認し、posに一番近い地面を返す
		for (int y = (int)pos.y; y > 0; y--)
		{
			if (map[(int)pos.x, y] == 0)
			{
				if (map[(int)pos.x, y - 1] == 1)
				{
					return new Vector3(pos.x, y + goalSize, 0);
				}
			}
		}
		return pos;
	}

	private void SetGoal()
	{
		switch (startPosition)
		{
			case 1:
			for (int x = 0; x < mapSize - goalSize; x++)
			{
				for (int y = 0; y < mapSize - goalSize; y++)
				{
					if (CheckSpace(x, y, goalSize))
					{
						Vector3 pos = new Vector3(x + goalSize / 2, y + goalSize / 2, 0);
						Instantiate(goal, ReturnGroundPosition(pos), Quaternion.identity);
						isGoalCreated = true;
						return;
					}
					else
					{
						continue;
					}
				}
			}
			break;
			case 2:
			for (int x = mapSize - goalSize; x > 0; x--)
			{
				for (int y = 0; y < mapSize - goalSize; y++)
				{
					if (CheckSpace(x, y, goalSize))
					{
						Vector3 pos = new Vector3(x + goalSize / 2, y + goalSize / 2, 0);
						Instantiate(goal, ReturnGroundPosition(pos), Quaternion.identity);
						isGoalCreated = true;
						return;
					}
					else
					{
						continue;
					}
				}
			}
			break;
			case 3:
			for (int x = 0; x < mapSize - goalSize; x++)
			{
				for (int y = mapSize - goalSize; y > 0; y--)
				{
					if (CheckSpace(x, y, goalSize))
					{
						Vector3 pos = new Vector3(x + goalSize / 2, y + goalSize / 2, 0);
						Instantiate(goal, ReturnGroundPosition(pos), Quaternion.identity);
						isGoalCreated = true;
						return;
					}
					else
					{
						continue;
					}
				}
			}
			break;
			case 4:
			for (int x = mapSize - goalSize; x > 0; x--)
			{
				for (int y = mapSize - goalSize; y > 0; y--)
				{
					if (CheckSpace(x, y, goalSize))
					{
						Vector3 pos = new Vector3(x + goalSize / 2, y + goalSize / 2, 0);
						Instantiate(goal, ReturnGroundPosition(pos), Quaternion.identity);
						isGoalCreated = true;
						return;
					}
					else
					{
						continue;
					}
				}
			}
			break;
		}
	}
	void Start()
	{
		digMapScript = GameObject.Find("Tilemap").GetComponent<DigMapScript>();
		goal = (GameObject)Resources.Load("Prefabs/Map/GoalArea");
		player = GameObject.Find("Player");
		startPosition = Random.Range(1, 5);

		if (digMapScript.randomSeed)
		{
			Random.InitState(System.DateTime.Now.Millisecond);
		}
		else
		{
			Random.InitState(digMapScript.seed);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (digMapScript.isMapCreated && !isGoalCreated)
		{
			map = digMapScript.map;
			SetGoal();
		}
	}
}
