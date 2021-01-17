using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 0.5f;
    Vector2 bacgroundVector;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        bacgroundVector = new Vector2(0, scrollingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += bacgroundVector * Time.deltaTime;
    }
}
