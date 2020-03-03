using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseEarth : MonoBehaviour
{
    [SerializeField]
    GameObject EarthPrefab;

    [SerializeField]
    float distance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.up = Vector3.up;
        GameObject g = Instantiate(EarthPrefab, this.transform.position + (new Vector3(this.transform.forward.x, 0f, this.transform.forward.z) * distance), Quaternion.identity);
    }
}
