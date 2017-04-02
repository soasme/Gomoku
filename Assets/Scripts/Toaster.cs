using UnityEngine;
using System.Collections;

public class Toaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Competetion competetion = FindObjectOfType<Competetion> ();
		if (competetion.isGameEnded ()) {
			if (competetion.winner == Chess.BLACK) {
				GetComponent<TextMesh> ().text = "Black Win.\nType Enter to restart.";
			} else {
				GetComponent<TextMesh> ().text = "White Win.\nType Enter to restart.";
			}
			GetComponent<Renderer> ().enabled = true;
			if (Input.GetKeyDown (KeyCode.Return)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("RoundScene");
			}
		}

	}

	public IEnumerator Toast(string text, float time) {
		GetComponent<Renderer> ().enabled = true;
		GetComponent<TextMesh> ().text = text;
		yield return new WaitForSeconds (time);
		GetComponent<TextMesh> ().text = "";
		GetComponent<Renderer> ().enabled = false;
	}
}
