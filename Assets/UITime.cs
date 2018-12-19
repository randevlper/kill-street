using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITime : MonoBehaviour {
	[SerializeField] TextMeshProUGUI text;
	[SerializeField] int maxLength = 6;
	private void Start() {
		GameManager.instance.OnTimerChange += OnTimerChange;
	}
	void OnTimerChange(float value){
		string s = value.ToString();
		if(s.Length > maxLength){
			s = s.Substring(0, maxLength);
		}
		text.text = s; 
	}
}
