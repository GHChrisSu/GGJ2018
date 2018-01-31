using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerA : MonoBehaviour {

    public ComputerB[] computersLinked;
    public BattleLine battleLine;
	// Use this for initialization
	void Start () {
        battleLine = Instantiate(battleLine) as BattleLine;
        battleLine.BattleStart(transform.position, computersLinked[0].transform.position);
        battleLine.BattleResultEvent += BattleResult;
        computersLinked[0].SetBattleResultEvent(battleLine);
    }

    private void BattleResult(string winner)
    {
        Debug.Log("ComputerA " + winner);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("GetKeyDown");
            battleLine.PlusRedHackCount();
        }
	}
}
