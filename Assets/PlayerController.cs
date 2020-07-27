using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rigidbody;
    private float movementSpeed = 5;
    public Text scoreText;
    private int score = 0;
    public ParticleSystem particleSystemPlayer;
    public ParticleSystem particleSystemEnemy;
    private bool gameOver = false;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update() {

        if (rigidbody.velocity.magnitude < 2) {
            particleSystemPlayer.Stop();
        }
        else if (!particleSystemPlayer.isPlaying) {
                particleSystemPlayer.Play();
        }

    }

    private void FixedUpdate() {
        if (!gameOver) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rigidbody.AddForce(movement * movementSpeed);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "Score: " + score;
        }
        if (other.gameObject.tag == "Enemy") {
            gameOver = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            particleSystemEnemy.Play();
            Destroy(other.gameObject, 1.5f);
            gameOverPanel.SetActive(true);
        }
    }
}
