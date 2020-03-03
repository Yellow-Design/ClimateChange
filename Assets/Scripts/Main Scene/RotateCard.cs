using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCard : MonoBehaviour
{
    bool flipped;
    Animator anim;
    void Start()
    {
        anim = this.GetComponentInParent<Animator>();
    }

    public void FlipCard()
    {
        switch (flipped)
        {
            case false:
                anim.SetTrigger("Flip");
                flipped = !flipped;
                break;

            case true:
                anim.SetTrigger("Reset");
                flipped = !flipped;
                break;
        }
    }
}
