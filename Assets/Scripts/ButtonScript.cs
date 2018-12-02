using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public Sprite normal;
    public Sprite hilighted;
    public Sprite pressed;

    public string value;

    private SpriteRenderer SR;
    private Sprite SRS;

    //enum state {normal, hilighted, pressed};
    public string state;

	// Use this for initialization
	void Start () {

        SR = GetComponent<SpriteRenderer>();
        if (SR.sprite == null)
        {
            SR.sprite = normal;
        }

        //SRS = SR.sprite;

        state = "normal";

	}
	
	// Update is called once per frame
	void Update () {
		
        if (state == "normal")
        {
            SR.sprite = normal;
        } else if (state == "hilighted")
        {
            SR.sprite = hilighted;
        } else if (state == "pressed")
        {
            SR.sprite = pressed;
        }

	}

    public void isHilighted()
    {
        //SRS = hilighted;
        //SR.sprite = hilighted;
        state = "hilighted";
    }

    public void isPressed()
    {
        //SR.sprite = pressed;
        state = "pressed";
    }

    public void isNormal()
    {
        //SR.sprite = normal;
        state = "normal";
    }

}
