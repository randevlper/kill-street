using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGrab : DeathObject {
	[SerializeField] Rigidbody2D rb2d;
	public override void OnClickStart () {
		DeathInput.instance.Attach(transform);
		rb2d.isKinematic = true;
	}

	public override void OnClickStay () {
	}

	public override void OnClickEnd () {
		transform.parent = null;
		rb2d.isKinematic = false;
	}
}