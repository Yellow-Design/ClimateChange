using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to make objects look like they are flashing. We do this by making the material on the object
/// slightly more transparent and back again.
/// </summary>
public class MaterialFlash : MonoBehaviour
{
    [SerializeField] // By using Serialize Field, we can change this value in the editor.
    float flashSpeed = 0.9f; // If you want the material to flash faster, change this value

    Material _active;
    Color _startColor;

    void Start()
    {
        _active = this.GetComponent<MeshRenderer>().material;
        _startColor = _active.GetColor("_BaseColor"); // '_BaseColor' is just the tag used in LWRP materials for the main material albedo color
        StartCoroutine(FlashOut());
    }

    IEnumerator FlashOut()
    {
        float t = 0;
        while (t < 1)
        {
            _active.SetColor("_BaseColor", Color.Lerp(_active.GetColor("_BaseColor"), new Color(_startColor.r, _startColor.g, _startColor.b, 0.4f), t));
            t += Time.deltaTime / flashSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FlashIn());
    }

    IEnumerator FlashIn()
    {
        float t = 0;
        while (t < 1)
        {
            _active.SetColor("_BaseColor", Color.Lerp(_active.GetColor("_BaseColor"), new Color(_startColor.r, _startColor.g, _startColor.b, 1f), t));
            t += Time.deltaTime / flashSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FlashOut());

    }
}
