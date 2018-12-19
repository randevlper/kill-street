using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInput : MonoBehaviour {

	public float clickOverlapRadius = 0.1f;
	public ContactFilter2D mask;

	public Camera mainCamera;
	Collider2D[] results;

	List<DeathObject> deathObjects;

	public static DeathInput instance;

	private void Awake () {
		mainCamera = Camera.main;
		results = new Collider2D[4];
		deathObjects = new List<DeathObject> ();
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Physics2D.OverlapCircle (mainCamera.ScreenToWorldPoint (Input.mousePosition), clickOverlapRadius, mask, results);
			foreach (var item in results) {
				if (item != null) {
					DeathObject dObject = item.GetComponent<DeathObject> ();
					if (dObject != null) {
						deathObjects.Add (dObject);
						dObject.OnClickStart ();
					}
				}
			}
		}
		if (Input.GetButtonUp ("Fire1")) {
			for (int i = 0; i < deathObjects.Count; i++) {
				deathObjects[i].OnClickEnd ();
				deathObjects.Remove(deathObjects[i]);
			}
		}

		foreach (var item in deathObjects) {
			item.OnClickStay ();
		}
		transform.position = (Vector2)mainCamera.ScreenToWorldPoint (Input.mousePosition);
	}

	public void Attach (Transform t) {
		t.parent = transform;
	}
}