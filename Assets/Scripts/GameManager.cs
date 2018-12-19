using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject human;
	public GameObject InvisibleWall;
	public Section oldSection;
	public Section currentSection;
	public Section nextSection;
	public GameObject[] sectionPrefabs;
	public static GameManager instance;

	public GameObject angelNotePrefab;
	public Gold.ObjectPool notepool;

	public GameObject poofPrefab;
	Gold.ObjectPool poofPool;

	int score;

	public Gold.Delegates.ActionValue<int> OnScoreChange;
	public Gold.Delegates.ActionValue<float> OnTimerChange;

	float timer;

	bool isTimerRunning;

	public int Score {
		get { return score; }
		set {
			score = value;
			OnScoreChange (score);
		}
	}

	public float Timer {
		get { return timer; }
		set {
			timer = value;
			OnTimerChange (timer);
		}
	}

	private void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		currentSection = Instantiate (sectionPrefabs[0], Vector3.zero, Quaternion.identity).GetComponent<Section> ();
		currentSection.gameObject.SetActive (true);
		notepool = new Gold.ObjectPool (angelNotePrefab, 10, true);
		poofPool = new Gold.ObjectPool (poofPrefab, 10, true);
		isTimerRunning = true;
		human.GetComponent<HumanInput> ().damageableBody.onDeath += OnHumanDeath;
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		float progress = currentSection.Progress (human.transform);
		float oldSectionProgress = 0;
		if (oldSection != null) {
			oldSectionProgress = oldSection.Progress (human.transform);
			Debug.Log (oldSectionProgress);
		}
		//Debug.Log("Progress: "  + progress);
		if (progress > 0.5 && nextSection == null) {
			//Create new section
			//Debug.Log("New Section!");
			nextSection = CreateSection ();
			nextSection.gameObject.SetActive (true);
		}
		if (progress >= 1.0) {
			//Debug.Log("Killing Section!");
			//currentSection.gameObject.SetActive(false);
			oldSection = currentSection;
			//Destroy(oldSection.gameObject);
			currentSection = nextSection;
			nextSection = null;
		}
		if (oldSectionProgress >= 1.75) {
			InvisibleWall.transform.position = currentSection.transform.position;
			Destroy (oldSection.gameObject);
		}

		if (isTimerRunning) {
			Timer += Time.deltaTime;
		}
	}

	public void OnHumanDeath (DamageInfo hit) {
		isTimerRunning = false;
	}

	public void Poof (Vector2 pos) {
		GameObject poof = poofPool.Get ();
		poof.SetActive (true);
		poof.transform.position = pos;
	}

	Section CreateSection () {
		return Instantiate (sectionPrefabs[Random.Range (0, sectionPrefabs.Length)], currentSection.end.position, Quaternion.identity).GetComponent<Section> ();
	}

}