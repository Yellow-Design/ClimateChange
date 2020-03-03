using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeThingsLookLikeTheyBeFloating : MonoBehaviour
{
    Vector3 _initialPosition;
    float t = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_initialPosition.x, _initialPosition.y + (Mathf.Cos(Time.time*2)/75), _initialPosition.z);
    }
}
