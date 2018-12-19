using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour {

	[SerializeField] DamageableBody human;
	[SerializeField] GameObject[] healthObjects;

	private void Start () {
		human.onHealthChange += OnHealthChange;
	}

	void OnHealthChange (float health) {
		for (int i = 0; i < healthObjects.Length; i++) {
			healthObjects[i].SetActive (false);
		}

		for (int i = 0; i < (int) health; i++) {
			healthObjects[i].SetActive (true);
		}

	}
}