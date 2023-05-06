namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using NCharacter;
	using UnityEngine;
	using UnityEngine.UI;

	public class ShowHPScript : MonoBehaviour
	{
		private float HP;

		[SerializeField]
		private Sprite hrt;

		[SerializeField]
		private Sprite halfHrt;

		[SerializeField]
		private Sprite noneHrt;
		[SerializeField]
		private GameObject noneHrtObj;
		private GameObject[] heartArray = new GameObject[20];
		private GameObject hearts;

		private Player player;

		private void SetHearts(int max)
		{
			int maxN = (int)Mathf.Floor(max / 2f);

			for (int i = 0; i < maxN; i++)
			{
				heartArray[i] = Instantiate(noneHrtObj, Vector3.zero, Quaternion.identity);
				heartArray[i].transform.SetParent(hearts.transform, false);
				heartArray[i].transform.localPosition = new Vector3(100 * i, 0, 0);
			}


		}
		private void UpdateHearts(int hp, int max)
		{
			int n = (int)Mathf.Floor(hp / 2f);
			int maxN = (int)Mathf.Floor(max / 2f);
			bool isHalf = false;

			for (int i = 0; i < maxN; i++)
			{
				heartArray[i].GetComponent<Image>().sprite = noneHrt;
			}

			if (hp % 2 != 0)
			{
				isHalf = true;
			}

			for (int i = 0; i < n; i++)
			{
				heartArray[i].GetComponent<Image>().sprite = hrt;
			}
			if (isHalf)
			{
				heartArray[n].GetComponent<Image>().sprite = halfHrt;
			}
		}

		void Start()
		{
			player = GameObject.Find("Player").GetComponent<Player>();

			hearts = GameObject.Find("Hearts");

			SetHearts(player.maxHp);
			UpdateHearts(player.hp, player.maxHp);
		}

		void Update()
		{
			UpdateHearts(player.hp, player.maxHp);
			Debug.Log(player.name);
		}
	}
}
