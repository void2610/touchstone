using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//イベントのリスナー側
public class GameEventListener : MonoBehaviour
{
	[Header("監視対象のイベント")]
	public GameEvent gameEvent;

	[Header("イベントが起こった時の関数")]
	public UnityEvent response;

	//アクティブ時に呼ばれる
	private void OnEnable()
	{
		gameEvent.RegisterListener(this);

	}

	//非アクティブ時に呼ばれる
	private void OnDisable()
	{
		gameEvent.UnregisterListener(this);
	}

	//登録している関数を実行する
	public void OnEventRaised()
	{
		response.Invoke();
	}
}