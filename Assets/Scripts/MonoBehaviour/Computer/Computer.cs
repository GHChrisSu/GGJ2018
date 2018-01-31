using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour {
    //public string id;
    public List<Computer> connections;
    public Player interactingPlayer;
    public bool interacting;
    [Space]
    public SpriteRenderer playerColorIndicator;
    public GameObject SignalIndicator; 
    [Space]
    //state
    public Player occupier;

    float colorBlendT = 0f;
    public AnimationCurve colorBlendCurve;

    public bool localOccupying = false;
    public float localOccupyTime = 1;
    float localProgressPerFixedUpdate
    {
        get
        {
            if(localOccupyTime >0)
                return Time.fixedDeltaTime / localOccupyTime;
            else
            {
                return 1;
            }
        }
    }
    public float localOccupyProgress;

    public float[] connectionTime;
    public float[] connectionProgress;

    public float dotPerSec = 5;
    public float dotBuffer = 0;

    private void Update()
    {
        if(dotBuffer<1)
            dotBuffer += dotPerSec * Time.deltaTime;

        Color occupierColor = Color.white;
        if (occupier != null)
        {
            occupierColor = occupier.color;
        }
        if (localOccupying)
        {
            colorBlendT += Time.deltaTime;
            if(colorBlendT > 1)
            {
                colorBlendT -= 1;
            }
            
            playerColorIndicator.color = Color.Lerp( occupierColor, interactingPlayer.color, colorBlendCurve.Evaluate(colorBlendT));
        }
        else
        {
            playerColorIndicator.color = occupierColor;
        }
    }
    private void FixedUpdate()
    {
        ScoreFixedUpdate();
    }

    public void Hack(Player hacker)
    {
        //first occupy the computer
        if(occupier!= hacker)
        {
            localOccupyProgress += localProgressPerFixedUpdate;
            if(localOccupyProgress >= 1)
            {
                OccupyByPlayer(hacker);
                localOccupying = false;
            }
        }
        else
        {
            //start hacking connected computers
            bool shot = false;
            for(int i = 0; i < connections.Count; i++)
            {
                if(dotBuffer > 1)
                {
                    shot = true;
                    GameObject go = GameObject.Instantiate(SignalIndicator);
                    SignalIndicator si = go.GetComponent<SignalIndicator>();
                    float curProgress = connectionProgress[i];
                    if(connections[i].occupier != hacker )
                        si.Initialize(this.transform.position, connections[i].transform.position, hacker.color,connectionProgress[i]);
                }

                if(connectionTime[i] > 0)
                    connectionProgress[i] += Time.fixedDeltaTime / connectionTime[i];
                else
                {
                    connectionProgress[i] = 1;
                }
                if(connectionProgress[i] >= 1)
                {
                    connections[i].OccupyByPlayer(hacker);
                    connections[i].Initialize();
                }
            }
            if (shot)
            {
                dotBuffer--;
            }
        }
    }

    public void Initialize()
    {
        localOccupyProgress = 0;
        connectionProgress = new float[connections.Count];
    }

    public void StartInteraction(Player player)
    {
        Debug.Log("Started Interaction");
        Initialize();
        interactingPlayer = player;
        interacting = true;

        if(occupier != player)
        {
            localOccupying = true;
        }
    }
    public void StopInteraction()
    {
        interactingPlayer = null;
        interacting = false;
        localOccupying = false;
    }

    public void OccupyByPlayer(Player player)
    {
        occupier = player;
        playerColorIndicator.color = player.color;
    }

    public void Interact()
    {

    }

    public void ScoreFixedUpdate()
    {
        if(occupier != null && (interactingPlayer == null || interactingPlayer == occupier) )
        {
            occupier.score++;
        }
    }
}
