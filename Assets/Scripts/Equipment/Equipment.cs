namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;
	using UnityEngine.UI;

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
		/// 装備が効果を発揮する時間の開始時間
		/// </summary>
		public float activeStartTime = 0;

		/// <summary>
		/// 装備が効果を発揮する時間の開始時の角度
		/// </summary>
		public float activeStartAngle = 0;

		/// <summary>
		/// 装備が効果を発揮する時間の開始時のマウス位置
		/// </summary>
		public Vector2 activeStartPosition = Vector2.zero;

		/// <summary>
		/// 装備が効果を発揮しているかどうか
		/// </summary>
		public bool isActive;

		/// <summary>
		/// マウスの角度
		/// </summary>
		public float angle;

		public GameObject player;

		public void CutHP(Character target, int atk)
		{
			target.hp -= atk;
			Debug.Log(target.name + "のHPが" + atk + "削れた");
		}

		public float getMouseAngle()
		{
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
			float res = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			return res;
		}

		public Vector2 getMousePosition()
		{
			Vector2 res = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
			return res;
		}

		public virtual IEnumerator Action()
		{
			yield break;
		}

		public virtual void Effect()
		{
		}

		public virtual void Oninvalid()
		{
		}

		public virtual void Start()
		{
			player = GameObject.Find("Player");

		}

		public virtual void Update()
		{
		}

		public virtual void FixedUpdate()
		{
		}
	}
}
