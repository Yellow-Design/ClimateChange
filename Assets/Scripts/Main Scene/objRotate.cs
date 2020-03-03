using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objRotate : MonoBehaviour {
    public static objRotate Instance;
    public float rotSpeed;
    public float autoRotateSpeed;

    [HideInInspector]
    public bool rotating = true;

	void Start () {
        Instance = this;
	}

    // On Mouse Drag is a standard unity method that is called when a mouse drag is detected by the player
    private void OnMouseDrag()
    {
        rotating = false;
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.forward, -rotX);
        rotating = true;
    }

    void Update () {
        if(rotating)
            this.transform.Rotate(Vector3.forward, Time.deltaTime * autoRotateSpeed);
	}

}
