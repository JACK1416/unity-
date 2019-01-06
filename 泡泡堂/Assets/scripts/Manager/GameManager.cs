using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float startDelay = 3f;
    public float endDelay = 3f;
    public GameObject playerPrefab;
    public PlayerManager[] players;

    private WaitForSeconds startWaits;
    private WaitForSeconds endWaits;

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

        StartCoroutine(GameLoop());
    }

    private IEnumerator RoundStarting()
    {
        ResetAllPlayer();
        DisablePlayerControl();

        yield return startWaits;
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

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


}
