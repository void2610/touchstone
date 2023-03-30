namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Equipment : MonoBehaviour
	{
		/// <summary>
		/// 装備の名前
		/// </summary>
		public string name;

		/// <summary>
		/// 装備を使用するためのキー
		/// </summary>
		public string actionKey;

		/// <summary>
		/// クールタイムの長さ
		/// </summary>
		public float coolTimeLength;

		/// <summary>
		/// クールタイム中かどうか
		/// </summary>
		public bool isCooling;

		/// <summary>
		/// 装備を使用可能かどうか
		/// </summary>
		public bool isEnable;

		/// <summary>
		/// 装備が効果を発揮する時間の長さ
		/// </summary>
		public float activeTimeLength;

		/// <summary>
		/// 装備が効果を発揮しているかどうか
		/// </summary>
		public bool isActive;

		/// <summary>
		/// 装備のアイコン画像
		/// </summary>
		public Sprite icon;

		public bool GetKeyOrMouse(string key)
		{
			if (key == "Mouse0" || key == "Mouse1")
			{
				return Input.GetMouseButtonDown(int.Parse(key.Substring(5)));
			}
			else
			{
				return Input.GetKeyDown(key);
			}
		}

		public virtual IEnumerator Action()
		{
			isActive = true;
			yield return new WaitForSeconds(activeTimeLength);
			isActive = false;
			isCooling = true;
			yield break;
		}

		public IEnumerator CoolDown()
		{
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		public virtual void Start()
		{
			name = "NoName";
			actionKey = "NoKey";
			coolTimeLength = 0.0f;
			isCooling = false;
			isEnable = true;
			activeTimeLength = 0.0f;
			isActive = false;

			icon = Resources.Load<Sprite>("Sprites/Equipment/" + name);
		}

		public virtual void Update()
		{
			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				StartCoroutine(Action());
				StartCoroutine(CoolDown());
			}
		}

		public virtual void FixedUpdate()
		{
			if (isActive && isEnable && !isCooling)
			{
				// 装備の効果を発揮する処理
				Debug.Log("Active");
			}
			if (isCooling)
			{
				// クールタイム中の処理
				Debug.Log("Cooling");
			}
		}
	}
}
