using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public string playerName;
    public Color color;

    PlayerInteraction playerInteraction;

    public int score = 0;

    private void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    public void BreakInteraction()
    {
        playerInteraction.BreakInteraction();
    }
}
