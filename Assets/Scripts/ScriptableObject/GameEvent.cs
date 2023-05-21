using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObject/GameEvent")]
public class GameEvent : ScriptableObject
{
	private List<GameEventListener> listeners =
	new List<GameEventListener>();

	//登録されている関数を実行する
	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised();
		}
	}

	//リスナーを追加する
	public void RegisterListener(GameEventListener listener)
	{
		listeners.Add(listener);
	}

	//リスナーを削除する
	public void UnregisterListener(GameEventListener listener)
	{
		listeners.Remove(listener);
	}
}