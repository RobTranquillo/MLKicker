using System;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class TorTrigger : MonoBehaviour
{
	public Text scoreText;
    public ParticleSystem winPS;
	public string compareTag = "Ball";
	
    private int _score;
    private GameController _game;

    public void Start()
    {
	    _game = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag(compareTag)) {
			IncreaseScore();
            OnTor();
		}
	}

	void OnTor() {
		if (winPS)
			winPS.Play();
		_game.ThrowBallIn();
	}
    public void IncreaseScore() {
        SetScore(++_score);
    }
    public void ResetScore() {
        SetScore(0);
    }

    private void SetScore(int v) {
        _score = v;
        if (scoreText)
            scoreText.text = (_score).ToString();
    }

    
}