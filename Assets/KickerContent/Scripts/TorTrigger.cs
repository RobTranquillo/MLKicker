using System;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class TorTrigger : MonoBehaviour
{
	public Text scoreText;
	int score;
    public ParticleSystem winPS;
	public string compareTag = "Ball";
    public GameObject ball;
    Vector3 startPosBall;

    public void Start()
    {
        startPosBall = ball.transform.position;
    }
    private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag(compareTag)) {
            ball.transform.position = startPosBall;
            IncreaseScore();
            OnTor();
		}
	}

	void OnTor() {
		if (winPS)
			winPS.Play();
	}
    public void IncreaseScore() {
        SetScore(++score);
    }
    public void ResetScore() {
        SetScore(0);
    }

    private void SetScore(int v) {
        score = v;
        if (scoreText)
            scoreText.text = (score).ToString();
    }

    
}