using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a utility script. All it does is let us toggle a bunch of objects using a button. Attach this to a button and assign your objects.
/// This toggle is directly linked to object status on play.
/// </summary>
public class GameobjectActiveSwitch : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ObjectsToToggle;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate () { onClick(); });
    }

    void onClick()
    {
        foreach (GameObject g in ObjectsToToggle)
            g.SetActive(!g.activeInHierarchy);
    }
}
