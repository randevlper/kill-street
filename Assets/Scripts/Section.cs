using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {
	public Transform end;

	private void Start() {
		Debug.Log(Width);
	}

	public float Progress(Transform point){
		return  Mathf.Abs(transform.position.x - point.position.x) / Width;
	}

	public float Width{
		get { return Mathf.Abs(transform.position.x - end.position.x); }
	}
}
