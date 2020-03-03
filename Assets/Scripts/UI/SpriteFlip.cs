using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    Camera cam;
    Material mat;
    SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        _renderer = this.GetComponent<SpriteRenderer>();
        mat = _renderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        if(mat.color.a > 0)
        {
            if(Vector3.Angle(cam.transform.forward, this.transform.forward) > 90)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
        }
    }
}
