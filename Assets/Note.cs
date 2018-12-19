using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	[SerializeField] float speed;
	[SerializeField] GameObject PoofPrefab;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-transform.right * speed * Time.deltaTime);
	}

	public void Shoot(Vector2 position){
		transform.position = position;
	}

	private void OnBecameInvisible() {
		gameObject.SetActive(false);
	}

	void Remove(){
				Instantiate(PoofPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		IDamageable damageable = other.GetComponent<IDamageable>();
		damageable?.Damage(new DamageInfo(1));
		gameObject.SetActive(false);
	}
}
