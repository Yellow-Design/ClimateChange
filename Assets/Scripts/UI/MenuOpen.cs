using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpen : MonoBehaviour
{
    Animator anim;
    bool open = false;

    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick()
    {
        // All we're doing here is triggering premade animations from the animator component found in the start method
        switch (open)
        {
            case true:
                anim.SetTrigger("Close");
                open = !open;
                break;
            case false:
                anim.SetTrigger("Open");
                open = !open;
                break;
        }
    }
}
