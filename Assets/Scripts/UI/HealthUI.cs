namespace NUI
{
	using UnityEngine;
	using TMPro;
	using UnityEngine.UI;
	using NManager;

	public class HealthUI : MonoBehaviour
	{
		[SerializeField]
		private Sprite hrt;
		[SerializeField]
		private Sprite noneHrt;
		[SerializeField]
		private GameObject noneHrtObj;
		private GameObject[] heartArray = new GameObject[20];
		public void SetHearts(int max)
		{
			for (int i = 0; i < heartArray.Length; i++)
			{
				if (heartArray[i] != null)
				{
					Destroy(heartArray[i]);
				}
			}
			for (int i = 0; i < max; i++)
			{
				heartArray[i] = Instantiate(noneHrtObj, Vector3.zero, Quaternion.identity, this.transform);
				heartArray[i].transform.localPosition = new Vector3(100 * i, 0, 0);
			}
			UpdateHearts(max, max);
		}

		public void UpdateHearts(int hp, int max)
		{
			for (int i = 0; i < max; i++)
			{
				heartArray[i].GetComponent<Image>().sprite = noneHrt;
			}
			for (int i = 0; i < hp; i++)
			{
				heartArray[i].GetComponent<Image>().sprite = hrt;
			}
		}

		private void Start()
		{
			SetHearts(3);
		}

		private void Update()
		{
			UpdateHearts(GameManager.instance.player.hp, 3);
		}
	}
}
