using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	public int x = 0;
	public int y = 0;
	public bool stop = false;

	// Use this for initialization
	void Start () {
		MoveTo (Board.GetCenter ());
	}

	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<Competetion> ().isGameEnded ()) {
			Destroy (gameObject);
			return;
		}
		this.DetectMove ();
		this.RenderCursor ();
	}

	public Vector2 GetIndex() {
		return new Vector2 ((float)this.x, (float)this.y);
	}

	public void MoveTo (Vector2 xy) {
		this.x = (int)xy.x;
		this.y = (int)xy.y;
	}

	private void DetectMove() {
		// Get Cursor Motion Controller Event
		// Get current player
		// Preemptive for one player, block another player
		string horizontal_axis_name;
		string vertical_axis_name;
		float horizontal, vertical;
		Competetion competition = FindObjectOfType<Competetion> ();
		if (competition.currentPlayer == Chess.BLACK) {
			horizontal_axis_name = "BlackHorizontal";
			vertical_axis_name = "BlackVertical";
		} else if (competition.currentPlayer == Chess.WHITE) {
			horizontal_axis_name = "WhiteHorizontal";
			vertical_axis_name = "WhiteVertical";
		} else {
			return;
		}

		if (Input.GetButtonUp (horizontal_axis_name)) {
			horizontal = Input.GetAxis (horizontal_axis_name);
			if (horizontal > 0) {
				this.x += 1;
			} else if (horizontal < 0) {
				this.x -= 1;
			}

			// FIXME: jump over chesses.

			if (this.x < 0) {
				this.x = 0;
			} else if (this.x >= Board.N) {
				this.x = Board.N - 1;
			}
		}

		if (Input.GetButtonUp (vertical_axis_name)) {
			vertical = Input.GetAxis (vertical_axis_name);

			if (vertical > 0) {
				this.y += 1;
			} else if (vertical < 0) {
				this.y -= 1;
			}

			// FIXME: jump over chesses.

			if (this.y < 0) {
				this.y = 0;
			} else if (this.y >= Board.N) {
				this.y = Board.N - 1;
			}
		}

	}

	private void RenderCursor() {
		this.transform.position = Board.ConvertXYToPosition (this.x, this.y);
	}

}
