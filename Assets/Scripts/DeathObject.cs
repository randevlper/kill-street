using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour {
	[SerializeField] AudioClip breakSound;
	public virtual void OnClickStart(){
		GameManager.instance.Poof(transform.position);
		GameManager.instance.Score += 10;
		SoundManager.instance.Play(breakSound, gameObject, 1.0f);
		Destroy(gameObject);
	}

	public virtual void OnClickStay(){

	}

	public virtual void OnClickEnd(){

	}
}
