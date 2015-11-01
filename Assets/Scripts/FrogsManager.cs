using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrogsManager : MonoBehaviour
{

	[SerializeField]
	protected LayerMask clickableLayers;

	[SerializeField]
	public float jumpDuration = 0.6f;

	[SerializeField]
	public GoEaseType jumpEaseType;

	public RectTransform finishPanel;
	public RectTransform helpMenuPanel;

	public Text turnsLabel;

	public bool IsEnd
	{
		get
		{
			var pads = FindObjectOfType<LillyPads>().Pads;
			// first 4 need to be frogs who are going left
			// and the last 4 need to be frogs who are going right
			for (int i = 0; i < 4; i++)
			{
				if (pads[i].frog == null || pads[i].frog.GoingRight) return false;
				if (pads[8 - i].frog == null || !pads[8 - i].frog.GoingRight) return false;
			}
			return true;
		}
	}

	public List<Frog> Frogs { get; private set; }

	public bool IsPlaying { get; private set; }

	private int turns;

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
		var pads = FindObjectOfType<LillyPads>().Pads;
		for (int i = 0; i < pads.Count; i++) pads[i].frog = null;

		// set them to correct positions
		for (int i = 0; i < Frogs.Count; i++)
		{
			Frogs[i].Initialize(i < 4 ? i : i + 1);
		}
		IsPlaying = true;
		finishPanel.gameObject.SetActive(false);

		// refresh turns
		turns = 0;
		turnsLabel.text = "Potez: " + turns;
	}

	public void IncreaseTurn()
	{
		turns++;
		turnsLabel.text = "Potez: " + turns;
	}

	public void CheckEnd()
	{
		if (IsEnd)
		{
			IsPlaying = false;
			finishPanel.gameObject.SetActive(true);
		}
	}

	public void ToggleHelpMenu(bool isActive = true)
	{
		helpMenuPanel.gameObject.SetActive(isActive);
	}

}
