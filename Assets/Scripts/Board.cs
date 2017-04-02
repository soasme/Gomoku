using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public const int N = 15;
	public const float scale = 0.5f;
	public const int HORIZONTAL = 1;
	public const int VERTICAL = 2;
	public const int SLASH = 3;
	public const int BACKSLASH = 4;
	public Vector2 INVALID_XY = new Vector2 (-1.0f, -1.0f);

	public GameObject blackPref = null;
	public GameObject whitePref = null;
	public Material material;
	public GameObject[,] board;

	// Use this for initialization
	void Start () {
		this.board = new GameObject[N, N];
		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				this.board [i, j] = null;
			}
		}
		for (int i = 0; i < N; i++) {
			DrawLine(ConvertXYToPosition(i, 0), ConvertXYToPosition(i, N - 1), "Row-" + i);
		}
		for (int j = 0; j < N; j++) {
			DrawLine (ConvertXYToPosition (0, j), ConvertXYToPosition (N - 1, j), "Column" + j);
		}
	}

	private void DrawLine(Vector2 start, Vector2 end, string name) {
		GameObject line = new GameObject ("Line-" + name);
		LineRenderer render = line.AddComponent<LineRenderer> ();
		render.SetColors (Color.black, Color.black);
		render.material = material;
		render.SetWidth (0.01f, 0.01f);
		render.SetPosition (0, new Vector3 (start.x, start.y, 0));
		render.SetPosition (1, new Vector3 (end.x, end.y, 0));
		render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		render.receiveShadows = false;
		line.transform.parent = this.transform.parent.Find ("Grid").transform;
	}

	public bool PlaceBlack(int x, int y) {
		if (board [x, y] != null) {
			return false;
		}
		GameObject black = Instantiate (this.blackPref);
		black.transform.position = ConvertXYToPosition (x, y);
		black.transform.parent = this.transform.parent.transform.Find ("Chesses").transform;
		this.board [x, y] = black;
		return true;
	}

	public bool PlaceWhite(int x, int y) {
		if (board [x, y] != null) {
			return false;
		}
		GameObject white = Instantiate (this.whitePref);
		white.transform.position = ConvertXYToPosition (x, y);
		white.transform.parent = this.transform.parent.transform.Find ("Chesses").transform;
		this.board [x, y] = white;
		return true;
	}
		
	public Vector2 GetEmptyXY(int x, int y) {
		// x, y is the start index
		// we will generate a new index that is empty and also near x, y
		if (x < 0 || x >= N || y < 0 || y >= N) {
			return INVALID_XY;
		} else if (board [x, y] == null) {
			return new Vector2 ((float)x, (float)y);
		} else {
			Vector2 xy = GetEmptyXY(x - 1, y);
			if (xy == INVALID_XY) {
				xy = GetEmptyXY (x + 1, y);
			}
			if (xy == INVALID_XY) {
				xy = GetEmptyXY (x, y - 1);
			}
			if (xy == INVALID_XY) {
				xy = GetEmptyXY (x, y + 1);
			}
			return xy;
		}
	}

	private int _FindNumberInADirection(int x, int y, int type, int direction) {
		if (x < 0 || x >= N || y < 0 || y >= N || board[x, y] == null ||
			type != board [x, y].GetComponent<Chess> ().chessType) {
			return 0;
		} else if (direction == HORIZONTAL) {
			return 1 + _FindNumberInADirection (x + 1, y, type, direction);
		} else if (direction == -HORIZONTAL) {
			return 1 + _FindNumberInADirection (x - 1, y, type, direction);
		} else if (direction == VERTICAL) {
			return 1 + _FindNumberInADirection (x, y + 1, type, direction);
		} else if (direction == -VERTICAL) {
			return 1 + _FindNumberInADirection (x, y - 1, type, direction);
		} else if (direction == SLASH) {
			return 1 + _FindNumberInADirection (x + 1, y + 1, type, direction);
		} else if (direction == -SLASH) {
			return 1 + _FindNumberInADirection (x - 1, y - 1, type, direction);
		} else if (direction == BACKSLASH) {
			return 1 + _FindNumberInADirection (x + 1, y - 1, type, direction);
		} else if (direction == -BACKSLASH) {
			return 1 + _FindNumberInADirection (x - 1, y + 1, type, direction);
		} else {
			return 0;
		}
	}

	private int _FindNumberInALine(int x, int y, int type, int direction) {
		return _FindNumberInADirection (x, y, type, direction) + _FindNumberInADirection (x, y, type, -direction) - 1;
	}

	public int FindNumberInALine(int x, int y, int direction) {
		int type = board [x, y].GetComponent<Chess> ().chessType;
		return _FindNumberInADirection (x, y, type, direction) + _FindNumberInADirection (x, y, type, -direction) - 1;
	}

	public static Vector2 ConvertXYToPosition(int x, int y) {
		return new Vector2 (x * scale - 5, y * scale - 4);// - new Vector2 (Screen.width / 2.0f, Screen.height / 2.0f);
	}

	public static Vector2 GetCenter() {
		return new Vector2 (N / 2.0f, N / 2.0f);
	}

}
