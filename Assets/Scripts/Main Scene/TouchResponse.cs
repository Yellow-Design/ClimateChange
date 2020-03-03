using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchResponse : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) // using TouchPhase.Began ensures that it's a tap and not a drag
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 1000f))
                {
                    if (hit.collider.tag == "BelfastCard")
                        hit.collider.GetComponent<RotateCard>().FlipCard();
                    if (hit.collider.tag == "Marker")
                        hit.collider.GetComponent<LinkCardToButton>().Click();
                }
            }
        }
    }
}
