using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOzoneOverlay : MonoBehaviour
{
    Material _OzoneOverlay;
    [SerializeField]
    bool _on = false;
    bool _active = false;
    float _fadeSpeed = 1f;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Ozone") != null)
        {
            _OzoneOverlay = GameObject.FindGameObjectWithTag("Ozone").GetComponent<MeshRenderer>().material;
        }
        else
            StartCoroutine(InitSpawnedObj());

        if (this.GetComponent<Button>())
        {
            btn = this.GetComponent<Button>();
            btn.onClick.AddListener(delegate { OnClick(); });
        }
    }

    void OnClick()
    {
        if(_OzoneOverlay)
            switch (_on)
            {
                case true:
                    StartCoroutine(FadeOzoneOut());
                    _on = !_on;
                    break;

                case false:
                    StartCoroutine(FadeOzoneIn());
                    _on = !_on;
                    break;
            }
    }

    IEnumerator InitSpawnedObj()
    {
        while (_OzoneOverlay == null)
        {
            if (GameObject.FindGameObjectWithTag("Ozone") != null)
            {
                _OzoneOverlay = GameObject.FindGameObjectWithTag("Ozone").GetComponent<MeshRenderer>().material;
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOzoneIn()
    {
        if(btn)
            btn.interactable = false;
        float t = 0;
        while (t < _fadeSpeed)
        {
            _OzoneOverlay.SetColor("_BaseColor", Color.Lerp(_OzoneOverlay.GetColor("_BaseColor"), new Color(1, 1, 1, 0.9f), t));
            t += Time.deltaTime / _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        if (btn)
            btn.interactable = true;
    }

    public IEnumerator FadeOzoneOut()
    {
        if (btn)
            btn.interactable = false;
        float t = 0;
        while (t < _fadeSpeed)
        {
            _OzoneOverlay.SetColor("_BaseColor", Color.Lerp(_OzoneOverlay.GetColor("_BaseColor"), new Color(1, 1, 1, 0), t));
            t += Time.deltaTime / _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        if (btn)
            btn.interactable = true;
    }

}
