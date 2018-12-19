using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDamage : DeathObject {

	[SerializeField] DamageableBody damageable;
	[SerializeField] float damage;

	public override void OnClickStart(){
		damageable.Damage(new DamageInfo(damage));
	}

	public override void OnClickStay(){

	}

	public override void OnClickEnd(){

	}
}
