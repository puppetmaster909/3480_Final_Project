using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public Sprite normal;
    public Sprite hilighted;
    public Sprite pressed;

    public string value;

    private SpriteRenderer SR;

	// Use this for initialization
	void Start () {

        SR = GetComponent<SpriteRenderer>();
        if (SR.sprite == null)
        {
            SR.sprite = normal;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void isHilighted()
    {
        SR.sprite = hilighted;
    }

    public void isPressed()
    {
        SR.sprite = pressed;
    }

    public void isNormal()
    {
        SR.sprite = normal;
    }

}
