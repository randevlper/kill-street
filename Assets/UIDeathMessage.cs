using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDeathMessage : MonoBehaviour {
	[SerializeField] TextMeshProUGUI text;
	[SerializeField] HumanInput input;
	private void Start () {
		text.enabled = false;
		input.OnCanReturn += DisplayMessage;
	}

	void DisplayMessage () {
		text.enabled = true;
	}
}