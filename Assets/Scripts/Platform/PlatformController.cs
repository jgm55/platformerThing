using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
    protected SpriteRenderer renderer;

	// Use this for initialization
	void Awake () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    //Call whenever the character color changes
    public void CharacterColorChanged(Color changedToColor)
    {
        if (changedToColor == renderer.color)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
