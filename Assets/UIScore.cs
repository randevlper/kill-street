using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour {
	[SerializeField] TextMeshProUGUI text;
	private void Start() {
		GameManager.instance.OnScoreChange += OnScoreChange;
	}
	void OnScoreChange(int value){
		text.text = value.ToString();
	}
}
