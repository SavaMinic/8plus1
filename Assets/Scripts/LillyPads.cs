using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LillyPads : MonoBehaviour
{

	public List<GameObject> Pads { get; private set; }
	public bool[] Occupied { get; private set; }

	void Awake()
	{
		Pads = new List<GameObject>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Pads.Add(transform.GetChild(i).gameObject);
		}
		Occupied = new bool[transform.childCount];
	}

}
