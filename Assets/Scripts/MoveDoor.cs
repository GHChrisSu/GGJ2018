using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDoor : MonoBehaviour {
    public enum Door {
        Right,
        Left
    }
    public static bool Moving { get; set; }
    public Door moveDirection;
    private float moveSpeed = 3f;
    private RawImage doorImage;

    // Use this for initialization
    void Start () {
		doorImage = GetComponent<RawImage>();
        Moving = false;
    }

    // Update is called once per frame
    void Update () {
        if (Moving)
        {
            if (moveDirection == Door.Left)
            {
                doorImage.rectTransform.Translate(new Vector3(-moveSpeed, 0, 0));
            }
            else
            {
                doorImage.rectTransform.Translate(new Vector3(moveSpeed, 0, 0));
            }
        }
    }
}
