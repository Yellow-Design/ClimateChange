using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHeatOverlay : MonoBehaviour
{
    Material _heatOverlay;
    [SerializeField]
    bool _on = false;
    bool _active = false;
    float _fadeSpeed = 1f;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Heat") != null)
        {
            _heatOverlay = GameObject.FindGameObjectWithTag("Heat").GetComponent<MeshRenderer>().material;
        }
        else
            StartCoroutine(InitSpawnedObj());

        btn = this.GetComponent<Button>();
        if (btn)
            btn.onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick()
    {
        if(_heatOverlay)
            switch (_on)
            {
                case true:
                    StartCoroutine(FadeHeatOut());
                    _on = !_on;
                    break;

                case false:
                    StartCoroutine(FadeHeatIn());
                    _on = !_on;
                    break;

            }

    }

    IEnumerator InitSpawnedObj()
    {
        while (_heatOverlay == null)
        {
            if (GameObject.FindGameObjectWithTag("Heat") != null)
            {
                _heatOverlay = GameObject.FindGameObjectWithTag("Heat").GetComponent<MeshRenderer>().material;
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeHeatIn()
    {
        if(btn)
            btn.interactable = false;
        float t = 0;
        while (t < _fadeSpeed)
        {
            _heatOverlay.SetColor("_BaseColor", Color.Lerp(_heatOverlay.GetColor("_BaseColor"), new Color(1, 1, 1, 0.9f), t));
            t += Time.deltaTime / _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        if (btn)
            btn.interactable = true;
    }

    public IEnumerator FadeHeatOut()
    {
        if (btn)
            btn.interactable = false;
        float t = 0;
        while (t < _fadeSpeed)
        {
            _heatOverlay.SetColor("_BaseColor", Color.Lerp(_heatOverlay.GetColor("_BaseColor"), new Color(1, 1, 1, 0), t));
            t += Time.deltaTime / _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        if (btn)
            btn.interactable = true;
    }

}
