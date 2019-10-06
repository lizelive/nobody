using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu( menuName = "Data/Dialogue")]
public class Dialogue : ScriptableObject
{
	[Multiline]
	public string text;

	public Dialogue next;
	public Dialogue[] options;
}
