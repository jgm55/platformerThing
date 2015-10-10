using UnityEngine;
using System.Collections;

public class YellowPlatformController : PlatformController
{
    // Use this for initialization
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.yellow;
    }
}