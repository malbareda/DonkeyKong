using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	#region Constants
	private float ENEMY_INTERVAL = 2.0f;
	#endregion Constants

	#region Unity Fields
	public Camera mainCamera;
	public Text scoreText;
	public Text gameOverText;
	public PlayerController player;
	public Transform enemyContainer;
	public GameObject enemyPrefab;
	#endregion Unity Fields

	#region Fields
	private int score;
	private float gameTimer;
	private bool gameOver;
	private float enemyTimer;
	#endregion Fields

	#region Methods
	public void Start ()
	{
		Time.timeScale = 1;

		player.OnHitGoomba += OnHitGoomba;
		player.OnHitSpike += OnGameOver;
		player.OnHitOrb += OnGameWin;
        player.OnHitHammer += OnHitHammer;
	}

	public void Update ()
	{
		if (gameOver)
		{
			if (Input.GetKeyDown("r"))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			return;
		}

		enemyTimer -= Time.deltaTime;
		if (enemyTimer <= 0)
		{
			enemyTimer = ENEMY_INTERVAL;

			GameObject enemyObject = GameObject.Instantiate<GameObject>(enemyPrefab);
			enemyObject.transform.SetParent(enemyContainer);
			enemyObject.transform.localPosition = Vector3.zero;
		}

		scoreText.text = "Score: " + score;

		if (player.transform.position.y < -10) OnGameOver();
	}

	private void OnHitGoomba ()
	{
		this.score += 100;
	}


    private void OnHitHammer()
    {
        player.hammerTime();

    }

    private void OnGameOver ()
	{
		gameOver = true;

		scoreText.enabled = false;
		gameOverText.enabled = true;

		gameOverText.text = "Game over!\nScore: " + score + "\nPress R to restart";

		Time.timeScale = 0;
	}

	private void OnGameWin ()
	{
		gameOver = true;

		scoreText.enabled = false;
		gameOverText.enabled = true;

		gameOverText.text = "You win!\nScore: " + score + "\nPress R to restart";

		Time.timeScale = 0;
	}
	#endregion Methods
}