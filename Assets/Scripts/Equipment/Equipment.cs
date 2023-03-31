namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

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

		public void CutHP(Character target, int atk)
		{
			target.hp -= atk;
			Debug.Log(target.name + "のHPが" + atk + "削れた");
		}

		public virtual IEnumerator Action()
		{
			yield break;
		}
		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void FixedUpdate()
		{
		}
	}
}
