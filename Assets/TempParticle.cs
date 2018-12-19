﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParticle : MonoBehaviour {
	[SerializeField] new ParticleSystem  particleSystem;
	
	// Update is called once per frame
	void Update () {
		if(!particleSystem.isPlaying){
			gameObject.SetActive(false);
		}
	}
}
