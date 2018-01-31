using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Player))]
public class PlayerInteraction : MonoBehaviour {
    Player player;
    Animator animator;

    Color playerColor
    {
        get
        {
            return player.color;
        }
    }
    new Collider2D collider2D;

    public bool isHacking
    {
        get
        {
            return animator.GetBool("hacking");
        }
    }
    public GameObject interactingObject;
    public Computer interactingComputer;

    public List<GameObject> objectsHoveringOver;

    

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsHoveringOver.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsHoveringOver.Remove(collision.gameObject);
    }

    public GameObject GetClosestHoveringObjectByTag(string tag)
    {
        GameObject result =null;
        foreach(GameObject cur in objectsHoveringOver)
        {
            if(cur.tag == tag)
            {
                if(result == null)
                {
                    result = cur;
                }
                else
                {
                    if((result.transform.position - this.transform.position).magnitude > (cur.transform.position - this.transform.position).magnitude)
                    {
                        result = cur;
                    }
                }
            }
        }

        return result;

    }

    private void FixedUpdate()
    {
        if (isHacking)
        {
            if(objectsHoveringOver.Contains(interactingObject))
                interactingComputer.Hack(this.player);
            else
            {
                BreakInteraction();
            }
        }
        else
        {
            if(interactingObject != null)
            {
                if(interactingObject.tag == "Computer")
                {
                    Computer interactingComputer = interactingObject.GetComponent<Computer>();
                    interactingComputer.interactingPlayer = null;
                    interactingObject = null;
                }
            }
        }
    }
    public void Interact()
    {
        if (isHacking)
        {
            if(interactingObject != null && interactingObject.tag == "Computer")
            {
                interactingObject.GetComponent<Computer>().Interact();
            }
        }
        else
        {
            interactingObject = GetClosestHoveringObjectByTag("Computer");
            if(interactingObject != null)
            {
                interactingComputer = interactingObject.GetComponent<Computer>();
                if (interactingComputer.interactingPlayer != null)
                {
                    interactingObject = null;
                    interactingComputer = null;
                }
                else
                {
                    interactingComputer.StartInteraction(this.player);
                    animator.SetBool("hacking", true);
                }
            }
        }
    }

    public void BreakInteraction()
    {
        if(isHacking)
        {
            animator.SetBool("hacking", false);
            interactingObject = null;
            if(interactingComputer)
                interactingComputer.StopInteraction();
            interactingComputer = null;
        }
       
    }
}
