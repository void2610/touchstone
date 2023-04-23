using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerScript : MonoBehaviour
{
	private GameObject player;
	private int[,] map;
	private int x = 10;
	private int y = 10;

	bool CheckArea(int x, int y, int radius)
	{
		for (int i = x - radius; i <= x + radius; i++)
		{
			for (int j = y - radius; j <= y + radius; j++)
			{
				if (i >= 0 && j >= 0 && i < 250 && j < 250)
				{
					continue;
				}
				if (map[i, j] == 1)
				{
					return false;
				}
			}
		}
		return false;
	}

	void Start()
	{
		player = GameObject.Find("Player");
		map = GameObject.Find("Tilemap").GetComponent<DigMapScript>().map;
		while (CheckArea(x, y, 2))
		{
			x = Random.Range(0, 250);
			y = Random.Range(0, 250);
		}
		player.transform.position = new Vector3(x, y, 0);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
