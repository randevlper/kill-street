using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour {

	[SerializeField] DamageableBody damageable;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Sprite idleSprite;
	[SerializeField] Sprite hitSprite;

	[SerializeField] float hurtLength;
	[SerializeField] float reloadTime = 0.25f;
	[SerializeField] AudioClip clipHurt;
	int score = 100;

	Coroutine R_hurt;
	public bool canShoot = true;

	private void Update() {
		if(canShoot){
			GameObject noteObject = GameManager.instance.notepool.Get();
			noteObject.transform.position = transform.position;
			noteObject.SetActive(true);
			canShoot = false;
			Invoke("Reload", reloadTime);
		}	
	}

	void Reload(){
		canShoot = true;
	}

	private void Start () {
		damageable.onDeath += Death;
		damageable.onDamage += OnHit;
		damageable.Restore();
	}

	void OnHit (DamageInfo hit) {
		spriteRenderer.sprite = hitSprite;
		SoundManager.instance.Play(clipHurt, gameObject, 1.0f);
		if(R_hurt != null){
			StopCoroutine(R_hurt);
		}
		R_hurt = StartCoroutine (Hurt (hurtLength));
	}

	IEnumerator Hurt (float time) {
		yield return new WaitForSeconds (time);
		spriteRenderer.sprite = idleSprite;
	}

	void Death (DamageInfo hit) {
		GameManager.instance.Poof(transform.position);
		GameManager.instance.Score += score;
		Destroy (gameObject);
	}
}