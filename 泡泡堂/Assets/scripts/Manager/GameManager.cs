using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float startDelay = 3f;
    public float endDelay = 3f;
    public GameObject playerPrefab;
    public PlayerManager[] players;
    public Text messageText;
    public int roundToWin = 5;

    private WaitForSeconds startWaits;
    private WaitForSeconds endWaits;
    private int roundNumber;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    private void Start()
    {
        startWaits = new WaitForSeconds(startDelay);
        endWaits = new WaitForSeconds(endDelay);

        SpawnAllPlayer();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllPlayer()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].instance =
                Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }

    private IEnumerator GameLoop()
    {

        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
            SceneManager.LoadScene(0);
        else
            StartCoroutine(GameLoop());
    }

    private IEnumerator RoundStarting()
    {
        ResetAllPlayer();
        DisablePlayerControl();

        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        yield return startWaits;

    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();

        messageText.text = string.Empty;

        while (!OnePlayerLeft())
        {
            yield return null;
        }

    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

        roundWinner = null;
        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.wins++;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        yield return endWaits;
    }

    private bool OnePlayerLeft()
    {
        int num = 0;

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                num++;
        }

        return num <= 1;
    }

    private void ResetAllPlayer()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }

    private void DisablePlayerControl()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }

    private void EnablePlayerControl()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }

    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                return players[i];
        }

        return null;
    }

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].wins == roundToWin)
                return players[i];
        }

        return null;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
            message = roundWinner.playerColorText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].playerColorText + ": " + players[i].wins + " WINS\n";
        }

        if (gameWinner != null)
            message = gameWinner.playerColorText + " WINS THE GAME!";

        return message;
    }


}
