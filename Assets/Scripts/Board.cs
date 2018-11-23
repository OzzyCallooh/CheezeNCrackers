using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {
	public Cell[] cells;
	public Sprite cheese;
	public Sprite cracker;
	public int state;
	public Text statusText;

	void Awake() {
		for (int i = 0; i < cells.Length; i++) {
			cells[i].board = this;
			cells[i].index = i;
		}
	}

	public void SelectCell(int index) {
		if (state > 1) {
			ResetGame();
			return;
		}
		if (cells[index].HasMark())
			return;
		if (state == 0) {
			cells[index].SetMark(cheese);
			state = 1;
			CheckForWinner();
		} else if (state == 1) {
			cells[index].SetMark(cracker);
			state = 0;
			CheckForWinner();
		}
		UpdateStatusText();
	}

	public bool IsFull() {
		for (int i = 0; i < cells.Length; i++)
			if (!cells[i].HasMark())
				return false;
		return true;
	}

	public bool DidWin(Sprite mark) {
		return
			   (cells[0].GetMark() == mark && cells[0].GetMark() == cells[1].GetMark() && cells[1].GetMark() == cells[2].GetMark())
			|| (cells[3].GetMark() == mark && cells[3].GetMark() == cells[4].GetMark() && cells[4].GetMark() == cells[5].GetMark())
			|| (cells[6].GetMark() == mark && cells[6].GetMark() == cells[7].GetMark() && cells[7].GetMark() == cells[8].GetMark())
			|| (cells[0].GetMark() == mark && cells[0].GetMark() == cells[3].GetMark() && cells[3].GetMark() == cells[6].GetMark())
			|| (cells[1].GetMark() == mark && cells[1].GetMark() == cells[4].GetMark() && cells[4].GetMark() == cells[7].GetMark())
			|| (cells[2].GetMark() == mark && cells[2].GetMark() == cells[5].GetMark() && cells[5].GetMark() == cells[8].GetMark())
			|| (cells[0].GetMark() == mark && cells[0].GetMark() == cells[4].GetMark() && cells[4].GetMark() == cells[8].GetMark())
			|| (cells[2].GetMark() == mark && cells[2].GetMark() == cells[4].GetMark() && cells[4].GetMark() == cells[6].GetMark());
	}

	public void CheckForWinner() {
		if (DidWin(cheese)) {
			state = 2;
		} else if (DidWin(cracker)) {
			state = 3;
		} else if (IsFull()) {
			state = 4;
		}
	}


	void Start () {
		ResetGame();
		UpdateStatusText();
	}
	
	void Update () {
		
	}

	public void UpdateStatusText() {
		if (state == 0) {
			statusText.text = "Turn: Cheese";
		} else if (state == 1) {
			statusText.text = "Turn: Cracker";
		} else if (state == 2) {
			statusText.text = "Cheese Wins!";
		} else if (state == 3) {
			statusText.text = "Cracker Wins!";
		} else if (state == 4) {
			statusText.text = "It's a tie!";
		}
	}

	public void ResetGame() {
		ClearBoard();
		state = 0;
		UpdateStatusText();
	}

	public void ClearBoard() {
		for (int i = 0; i < cells.Length; i++)
			cells[i].Reset();
	}
}
