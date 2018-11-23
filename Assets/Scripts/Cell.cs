using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
	private bool hasMark;
	public Board board;
	public int index;
	public Image image;
	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
		GetComponent<Button>().onClick.AddListener(() => {
			if (this.board)
				this.board.SelectCell(this.index);
		});
	}

	public void Reset() {
		//image.sprite = null;
		//image.color = new Color(1f, 0f, 0f, 0f);
		if (HasMark())
			anim.SetTrigger("Removed");
		hasMark = false;
	}

	public void SetMark(Sprite mark) {
		hasMark = true;
		image.color = Color.white;
		image.sprite = mark;
		anim.SetTrigger("Placed");
	}

	public bool HasMark() {
		return hasMark;
	}

	public Sprite GetMark() {
		return hasMark ? image.sprite : null;
	}
}
