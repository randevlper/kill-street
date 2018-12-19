using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInput : MonoBehaviour {

	public Rigidbody2D rb2D;
	public DamageableBody damageableBody;
	public SpriteRenderer spriteRenderer;
	public Animator animator;

	public float deathWait = 2;
	public LevelManager levelManager;

	public float playerHeight = 1;
	public float playerWidth = 1;
	public float skinWidth = 0.05f;
	public float groundDetectDist = 0.25f;
	public float jumpSpeed = 5;
	public float runSpeed = 10;
	public bool isOnGround;
	public bool isOnSideLeft;
	public bool isOnSideRight;

	public LayerMask mask;

	public bool canPlayerMove = true;
	public bool canReturn;

	public System.Action OnCanReturn;
	public AudioClip clipHurt;

	private void Start () {
		damageableBody.Restore ();
		damageableBody.onDeath += OnDeath;
		damageableBody.onDamage += OnHit;
	}

	void OnHit(DamageInfo hit){
		SoundManager.instance.Play(clipHurt, gameObject, 1.0f);
	}

	void OnDeath (DamageInfo hit) {
		canPlayerMove = false;
		animator.SetBool("Dead", true);
		//spriteRenderer.enabled = false;
		GameManager.instance.Poof (transform.position);
		Invoke ("ReturnToMainMenu", deathWait);
	}

	void ReturnToMainMenu () {
		//levelManager.LoadLevel (0);
		canReturn = true;
		OnCanReturn?.Invoke ();
	}

	// Update is called once per frame
	void Update () {

		if (canPlayerMove) {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 newVelocity = rb2D.velocity;
			isOnGround = OnGround ();
			isOnSideLeft = OnSide (true);
			isOnSideRight = OnSide (false);

			if (Input.GetButtonDown ("Jump") && isOnGround) {
				newVelocity.y = jumpSpeed;
			}

			if (Mathf.Sign (input.x) > 0 && !isOnSideRight) {
				newVelocity.x = input.x * 10;
			} else if (Mathf.Sign (input.x) < 0 && !isOnSideLeft) {
				newVelocity.x = input.x * 10;
			} else {
				newVelocity.x = 0;
			}

			if (input.x != 0) {

				spriteRenderer.flipX = (Mathf.Sign (input.x) > 0 ? true : false);
			}
			animator.SetFloat ("Speed", Mathf.Abs (input.x));

			rb2D.velocity = newVelocity;
		} else {
			if (Input.anyKeyDown && canReturn) {
				levelManager.LoadLevel (0);
			}
		}

	}

	bool OnGround () {
		Vector3 start = transform.position - new Vector3 (playerWidth / 2, ((playerHeight / 2) - skinWidth));
		Vector3 start2 = transform.position - new Vector3 (-playerWidth / 2, ((playerHeight / 2) - skinWidth));
		float distance = groundDetectDist;
		Vector3 dir = -transform.up;
		Vector3 end = start + (dir * distance);
		Vector3 end2 = start2 + (dir * distance);
		Debug.DrawLine (start, end, Color.red);
		Debug.DrawLine (start2, end2, Color.red);
		return Physics2D.Raycast (start, dir, distance, mask) || Physics2D.Raycast (start2, dir, distance, mask);
	}

	bool OnSide (bool isLeft) {
		Vector3 start;
		Vector3 start2;
		float distance = groundDetectDist;
		Vector3 dir;
		if (isLeft) {
			start = transform.position - new Vector3 (((playerWidth / 2) - skinWidth), playerHeight / 2);
			start2 = transform.position - new Vector3 (((playerWidth / 2) - skinWidth), -playerHeight / 2);
			dir = -transform.right;
		} else {
			start = transform.position + new Vector3 (((playerWidth / 2) - skinWidth), playerHeight / 2);
			start2 = transform.position + new Vector3 (((playerWidth / 2) - skinWidth), -playerHeight / 2);
			dir = transform.right;
		}
		Vector3 end = start + (dir * distance);
		Vector3 end2 = start2 + (dir * distance);
		Debug.DrawLine (start, end, Color.blue);
		Debug.DrawLine (start2, end2, Color.blue);
		return Physics2D.Raycast (start, dir, distance, mask) || Physics2D.Raycast (start2, dir, distance, mask);
	}
}