using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalIndicator : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public Vector3 start;
    public Vector3 end;
    public float totalTime;
    public float tPerUpdate
    {
        get
        {
            if(totalTime == 0)
            {
                return 1;
            }
            return Time.fixedDeltaTime / totalTime;
        }
    }
    public float t;
    public AnimationCurve animationCurve;
    public AnimationCurve sizeOverLifetime;

	public void Initialize(Vector3 start, Vector3 end, Color color,float percentageByOne)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = FindObjectOfType<SpriteRenderer>();
        }
        spriteRenderer.color = color;
        this.start = start;
        this.end = end;
        t = 0;
        this.transform.position = start;
        
        this.transform.localScale = Vector3.one * sizeOverLifetime.Evaluate( Mathf.Clamp01 (percentageByOne));
    }

    private void Update()
    {
        t += tPerUpdate;
        this.transform.position = Vector2.Lerp(start, end, animationCurve.Evaluate(t));
        if(t >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
