using UnityEngine;
using System.Collections;

public class Data  
{
	public readonly static Data Instance = new Data();

	public int score = 0;
	public string name = string.Empty;
}