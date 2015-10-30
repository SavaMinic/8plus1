using UnityEngine;
using System.Collections;

public class LillyPad : MonoBehaviour
{

	public int index;
	public Frog frog;

	public bool IsOccupied { get { return frog != null; } }
}
