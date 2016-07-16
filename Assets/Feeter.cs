using UnityEngine;
using System.Collections;

public class Feeter : MonoBehaviour {
	public float standingSpread;
	public float maxStep;
	public Transform[] feets;

	int stIndex;
	int endIndex;

	// Use this for initialization
	void Start() {
		float offset = -feets.Length / 2f;
		foreach (Transform foot in feets) {
			foot.position = transform.position + new Vector3(offset * standingSpread, 0, 0);
			offset += 1f;
		}
		stIndex = 0;
		endIndex = feets.Length-1;
	}

	// Update is called once per frame
	void Update() {
		if (feets[endIndex].position.x > transform.position.x + maxStep) {
			feets[endIndex].position = feets[stIndex].position - new Vector3(standingSpread, 0, 0);
			stIndex = wrapFeetsInd(stIndex - 1);
			endIndex = wrapFeetsInd(stIndex - 1);
		}
		if (feets[stIndex].position.x < transform.position.x + maxStep) {
			feets[stIndex].position = feets[endIndex].position + new Vector3(standingSpread, 0, 0);
			stIndex = wrapFeetsInd(stIndex + 1);
			endIndex = wrapFeetsInd(stIndex + 1);
		}
	}

	int wrapFeetsInd(int ind) {
		if(ind<0) { return ind + feets.Length; }
		if(ind >= feets.Length) { return ind - feets.Length; }
		return ind;
	}
}
