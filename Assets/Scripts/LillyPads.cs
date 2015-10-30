using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LillyPads : MonoBehaviour
{

	public List<LillyPad> Pads { get; private set; }

	void Awake()
	{
		Pads = new List<LillyPad>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Pads.Add(transform.GetChild(i).gameObject.GetComponent<LillyPad>());
			Pads[i].index = i;
			Pads[i].frog = null;
		}
	}

}
