using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrogsManager : MonoBehaviour
{

	[SerializeField]
	protected LayerMask clickableLayers;

	[SerializeField]
	public float jumpDuration = 0.6f;

	[SerializeField]
	public GoEaseType jumpEaseType;

	public List<Frog> Frogs { get; private set; }

	public bool IsPlaying { get; private set; }

	void Awake()
	{
		Frogs = new List<Frog>();
		for (int i = 0; i < transform.childCount; i++)
		{
			var frog = transform.GetChild(i).gameObject.GetComponent<Frog>();
			Frogs.Add(frog);
		}
	}

	void Start()
	{
		NewGame();
	}

	public void NewGame()
	{
		var pads = FindObjectOfType<LillyPads>();
		for (int i = 0; i < pads.Pads.Count; i++) pads.Pads[i].frog = null;

		// set them to correct positions
		for (int i = 0; i < Frogs.Count; i++)
		{
			Frogs[i].Initialize(i < 4 ? i : i + 1);
		}
		IsPlaying = true;
	}

	public void CheckEnd()
	{
		
	}

}
