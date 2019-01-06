using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager
{
    [HideInInspector] public int playerNumber;
    [HideInInspector] public GameObject instance;
    public Transform spawnPoint;
    public Color playerColor;

    private Movement movement;
    private DropBomb dropBomb;

    public void Setup()
    {
        movement = instance.GetComponent<Movement>();
        dropBomb = instance.GetComponent<DropBomb>();

        movement.playerNumber = playerNumber;
        dropBomb.playerNumber = playerNumber;

        SkinnedMeshRenderer[] renderers = instance.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = playerColor;
        }
    }

    public void DisableControl()
    {
        movement.enabled = false;
        dropBomb.enabled = false;

    }

    public void EnableControl()
    {
        movement.enabled = true;
        dropBomb.enabled = true;
    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        //instance.SetActive(false);
        instance.SetActive(true);
    }
}
