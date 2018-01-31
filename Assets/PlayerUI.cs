using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    Player player;
    public Text label;
    public Text score;
    
    private void Start()
    {
        player = GetComponent<Player>();
        label.color = player.color;
        score.color = player.color;
    }
    private void Update()
    {
        score.text = player.score.ToString();
    }
}
