using UnityEngine;
using System.Collections;

public class RedPlatformController : PlatformController
{
    // Use this for initialization
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
    }
}