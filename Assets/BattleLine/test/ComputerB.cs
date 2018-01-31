using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerB : MonoBehaviour {

    public GameObject[] computersLinked;
    [Range(0,1)]
    private float hackProcess;
    private BattleLine battleLine;
    // Use this for initialization
    void Start () {

    }

    public void SetBattleResultEvent(BattleLine battleLine)
    {
        this.battleLine = battleLine;
        battleLine.BattleResultEvent += BattleResult;
    }

    private void BattleResult(string winner)
    {
        Debug.Log("ComputerB " + winner);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("GetKeyDown");
            battleLine.PlusGreenHackCount();
        }
	}
}
