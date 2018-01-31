using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLine : MonoBehaviour {
    private static float MAXAMP = 4.8f;
    private int redHackCount = 1;
    private int greenHackCount = 1;

    private float battleTime = 60f;
    private bool isBattleStart = false;

    private Transform redLine;
    private Transform greenLine;
    private Renderer redRender;
    private Renderer greenRender;

    private float redScaleZ;
    private float greenScaleZ;
    private float totalScale;

    public delegate void BattleResult(string winner);
    public event BattleResult BattleResultEvent;

    public void PlusRedHackCount()
    {
        redHackCount++;
    }

    public void PlusGreenHackCount()
    {
        greenHackCount++;
    }

    public void BattleStart(Vector2 redPos, Vector2 greenPos)
    {
        redLine = transform.Find("BattleLineRed");
        redLine.position = redPos;
        redRender = redLine.gameObject.GetComponent<Renderer>();

        greenLine = transform.Find("BattleLineGreen");
        greenLine.position = greenPos;
        greenRender = greenLine.gameObject.GetComponent<Renderer>();

        Vector2 middlePos = (redPos + greenPos) / 2;
        float scaleRatio = (float)((greenPos - middlePos).magnitude * 0.13 / 0.053);

        greenLine.forward = greenLine.position - (Vector3)middlePos;
        greenLine.Rotate(0, 0, 90);
        redLine.forward = redLine.position - (Vector3)middlePos;
        redLine.Rotate(0, 0, 90);

        redScaleZ = scaleRatio * redLine.localScale.z;
        greenScaleZ = scaleRatio * greenLine.localScale.z;

        totalScale = redScaleZ + greenScaleZ;
        isBattleStart = true;
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        if (isBattleStart == true)
        {
            int totalCount = redHackCount + greenHackCount;
            battleTime -= Time.deltaTime;

            redRender.material.SetFloat("_Magnitude", ((float)redHackCount / (float)totalCount) * MAXAMP);
            redLine.localScale = new Vector3(redLine.localScale.x, redLine.localScale.y, ((float)redHackCount / (float)totalCount) * totalScale);

            greenRender.material.SetFloat("_Magnitude", ((float)greenHackCount / (float)totalCount) * MAXAMP);
            greenLine.localScale = new Vector3(greenLine.localScale.x, greenLine.localScale.y, ((float)greenHackCount / (float)totalCount) * totalScale);

            if (battleTime <= 0)
            {
                isBattleStart = false;
                battleTime = 5f;
                if (redHackCount > greenHackCount)
                {
                    redRender.material.SetFloat("_Magnitude", 0);
                    redLine.localScale = new Vector3(redLine.localScale.x, redLine.localScale.y, totalScale);
                    greenLine.localScale = new Vector3(0, 0, 0);
                    BattleResultEvent.Invoke("red");
                }
                else
                {
                    greenRender.material.SetFloat("_Magnitude", 0);
                    greenLine.localScale = new Vector3(greenLine.localScale.x, greenLine.localScale.y, totalScale);
                    redLine.localScale = new Vector3(0, 0, 0);
                    BattleResultEvent.Invoke("green");
                }
            }
        }
	}
}
