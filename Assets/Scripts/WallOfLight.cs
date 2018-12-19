using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfLight : MonoBehaviour {
	public LevelManager levelManager;
	float speed = 1f;
	float maxSpeed = 7.0f;
	float mult = 0.1f;

	// Update is called once per frame
	void Update () {
		transform.Translate (transform.right * speed * Time.deltaTime);
		speed = Mathf.Clamp(speed + (mult * Time.deltaTime), 0, maxSpeed);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		Debug.Log (other);
		HumanInput human = other.gameObject.GetComponent<HumanInput> ();
		if (human != null) {
			human.damageableBody.Damage (new DamageInfo (1, true));
		}
	}
}