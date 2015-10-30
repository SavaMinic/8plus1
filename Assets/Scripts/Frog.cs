using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour
{
	public bool GoingRight { get; private set; }
	public int CurrentIndex { get; private set; }

	private Animator frogAnimator;
	private LillyPads pads;
	private FrogsManager manager;

	void Awake()
	{
		frogAnimator = GetComponent<Animator>();
		frogAnimator.speed = 0f;

		pads = FindObjectOfType<LillyPads>();
		manager = FindObjectOfType<FrogsManager>();
	}

	public void Initialize(int index)
	{
		GoingRight = index < 4;
		CurrentIndex = -1;
		SetIndex(index);

		// don't need to change z
		transform.position = (Vector2)pads.Pads[index].transform.position;
		transform.rotation = Quaternion.Euler(0f, 0f, GoingRight ? 270f : 90f);
	}

	public void SetIndex(int index)
	{
		if (CurrentIndex != -1) pads.Pads[CurrentIndex].frog = null;
		CurrentIndex = index;
		pads.Pads[CurrentIndex].frog = this;
	}

	void OnMouseUpAsButton()
	{
		// try just one ahead
		bool isMoved = TryToMove();
		// if it can't, try to jump over him
		if (!isMoved) TryToMove(2);
	}

	private bool TryToMove(int howMuch = 1)
	{
		int nextIndex = CurrentIndex + (GoingRight ? 1 : -1) * howMuch;
		// check bounds
		if (nextIndex >= 0 && nextIndex <=8 && !pads.Pads[nextIndex].IsOccupied)
		{
			Move(nextIndex);
			return true;
		}
		return false;
	}

	private void Move(int index)
	{
		SetIndex(index);
		var target = (Vector2) pads.Pads[index].transform.position;

		var t = Go.to(transform, manager.jumpDuration, new GoTweenConfig().position(target).setEaseType(manager.jumpEaseType));
		t.setOnBeginHandler(tween =>
		{
			frogAnimator.speed = 1f;
		});
		t.setOnCompleteHandler(tween =>
		{
			frogAnimator.speed = 0f;
		});
	}
}
