using UnityEngine;
using UnityEngine.UI;

public class GameLifecycle : MonoBehaviour {
    public PlayerController Player;
    public CameraController Camera;
    public Text WinTextObject;
    public Text ScoreTextObject;
    public Button ResetButton;

    private GameObject[] mPickups;

    private const int MaxItems = 13;
    private bool mRunning = true;

	public void Start () {
        mPickups = GameObject.FindGameObjectsWithTag("Pickup");
        ResetButton.onClick.AddListener(OnRestartClicked);
        Player.OnScoreIncreased += OnPlayerScoreIncreased;

        RestartGame();
    }

    public void OnDisable( )
    {
        Player.OnScoreIncreased -= OnPlayerScoreIncreased;
    }

    public void LateUpdate () {
        if (!mRunning)
        {
            return;
        }

        if (Player.Score >= MaxItems /* || IsTimeUp */)
        {
            EndGame();
        }
	}

    public void OnRestartClicked()
    {
        RestartGame();
    }

    private void EndGame()
    {
        mRunning = false;
        Camera.SetMoveable(false);
        Player.SetEnabled(false);

        if (Player.Score >= MaxItems)
        {
            WinTextObject.text = "You Win!";
        }
        else
        {
            WinTextObject.text = "You Lose!";

        }

        WinTextObject.gameObject.SetActive(true);
        ResetButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        mRunning = true;
        Player.Score = 0;
        Camera.SetMoveable(true);
        Player.ResetPlayer();

        ResetButton.gameObject.SetActive(false);
        WinTextObject.gameObject.SetActive(false);
        OnPlayerScoreIncreased();

        foreach (GameObject pickup in mPickups)
        {
            pickup.gameObject.SetActive(true);
        }
    }

    private void OnPlayerScoreIncreased()
    {
        ScoreTextObject.text = "Score: " + Player.Score.ToString();
    }
}
