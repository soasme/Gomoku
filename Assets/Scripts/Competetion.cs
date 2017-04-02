using UnityEngine;
using System.Collections;

public class Competetion : MonoBehaviour {

	public const int GAME_STATE_RUNNING = 0;
	public const int GAME_STATE_END = 1;

	public int currentPlayer;
	public int winner = Chess.EMPTY;
	public int gameState = GAME_STATE_RUNNING;

	// Use this for initialization
	void Start () {
		currentPlayer = Chess.BLACK;
	}

	private int GetWinner(int x, int y) {
		Board board = FindObjectOfType<Board> ();
		if (board.FindNumberInALine (x, y, Board.HORIZONTAL) == 5 ||
			board.FindNumberInALine (x, y, Board.VERTICAL) == 5 ||
			board.FindNumberInALine (x, y, Board.SLASH) == 5 ||
			board.FindNumberInALine (x, y, Board.BACKSLASH) == 5) {
			return board.board [x, y].GetComponent<Chess> ().chessType;
		} else {
			return Chess.EMPTY;
		}
	}

	private void SwitchCurrentPlayer() {
		if (this.currentPlayer == Chess.BLACK) {
			this.currentPlayer = Chess.WHITE;
		} else if (this.currentPlayer == Chess.WHITE) {
			this.currentPlayer = Chess.BLACK;
		}
	}

	private void CheckWinner(int x, int y) {
		winner = GetWinner (x, y);
		if (winner == Chess.BLACK) {
			gameState = GAME_STATE_END;
		} else if (winner == Chess.WHITE) {
			gameState = GAME_STATE_END;
		}
	}

	public bool isGameEnded() {
		return this.gameState == GAME_STATE_END;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GAME_STATE_END) {
			return;
		}

		Board board = FindObjectOfType<Board> ();
		Cursor cursor = FindObjectOfType<Cursor> ();
		Vector2 index;

		if (this.currentPlayer == Chess.BLACK) {
			if (Input.GetButtonUp ("BlackFire")) {
				index = cursor.GetIndex ();
				if (board.PlaceBlack ((int)index.x, (int)index.y)) {
					cursor.MoveTo (board.GetEmptyXY ((int)index.x, (int)index.y));
					CheckWinner ((int)index.x, (int)index.y);
					SwitchCurrentPlayer ();
				} else {
					FindObjectOfType<Toaster> ().Toast ("Invalid Position", 3);
				}
			}
		} else if (this.currentPlayer == Chess.WHITE) {
			if (Input.GetButtonUp ("WhiteFire")) {
				index = cursor.GetIndex ();
				if (board.PlaceWhite ((int)index.x, (int)index.y)) {
					cursor.MoveTo(board.GetEmptyXY((int)index.x, (int)index.y));
					CheckWinner((int)index.x, (int)index.y);
					SwitchCurrentPlayer ();
				} else {
					StartCoroutine(FindObjectOfType<Toaster> ().Toast ("Invalid Position", 3));
				}
			}
		}
	}
			
}
