using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObject/IntVariable")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize()
	{
	}
}