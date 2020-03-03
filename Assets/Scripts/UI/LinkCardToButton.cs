using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkCardToButton : MonoBehaviour
{
    [SerializeField]
    Cards CardToLink;

    [SerializeField]
    Option InteractionType;

    GameObject _card;
    Material _line;
    Material _cardMat;
    bool _on = false;
    Button _btn;
    float _fadeSpeed = 1f;
    List<LinkCardToButton> _otherButtons = new List<LinkCardToButton>();

    // Start is called before the first frame update
    void Start()
    {
        if(InteractionType == Option.Button)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Button");
            foreach(GameObject g in temp)
            {
                LinkCardToButton t = g.GetComponent<LinkCardToButton>();
                if (t != this)
                    _otherButtons.Add(t);
            }
            _btn = this.GetComponent<Button>();
            _btn.onClick.AddListener(delegate { Click(); });
        }

        if(InteractionType == Option.Marker)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Marker");
            foreach (GameObject g in temp)
            {
                LinkCardToButton t = g.GetComponent<LinkCardToButton>();
                if (t != this)
                    _otherButtons.Add(t);
            }
        }


        StartCoroutine(InitSpawnedObj());

    }

    public void CloseCard()
    {
        StartCoroutine(FadeCardOut());
        foreach (LinkCardToButton btn in _otherButtons)
        {
            if (this.CardToLink == Cards.Heatwaves)
                StartCoroutine(this.GetComponent<ToggleHeatOverlay>().FadeHeatOut());
            if (this.CardToLink == Cards.OzoneCard)
                StartCoroutine(this.GetComponent<ToggleOzoneOverlay>().FadeOzoneOut());
        }
    }

    public void Click()
    {
        if(_card)
            switch (_on)
            {
                case true:
                    StartCoroutine(FadeCardOut());
                    break;

                case false:
                    foreach (LinkCardToButton oc in _otherButtons)
                        oc.CloseCard();
                    StartCoroutine(FadeCardIn());
                    break;

            }
    }

    IEnumerator FadeCardIn()
    {
        objRotate.Instance.rotating = false;
        bool correctAngle = true;
        GameObject earth = GameObject.FindGameObjectWithTag("AR Object").transform.GetChild(0).gameObject;
        Color _lineStart = _line.GetColor("_BaseColor");
        if (_btn)
            _btn.interactable = false;

        float t = 0;
        //float initialAngle = Vector3.Angle(_card.transform.GetChild(0).forward, _card.transform.GetChild(0).transform.position - Camera.main.transform.position);
        while (t < 1 || !correctAngle)
        {
            //float angle = Vector3.Angle(_card.transform.GetChild(0).forward, _card.transform.GetChild(0).transform.position - Camera.main.transform.forward);
            //Debug.Log(angle);
            /*if (angle < 170 && angle > 10)
                if(initialAngle < 71)
                    earth.transform.Rotate(Vector3.forward, Time.deltaTime * 100);
                else
                    earth.transform.Rotate(-Vector3.forward, Time.deltaTime * 100);

            else
                correctAngle = true;*/

            _line.SetColor("_BaseColor", Color.Lerp(_line.GetColor("_BaseColor"), new Color(_lineStart.r, _lineStart.g, _lineStart.b, 1f), t));
            _cardMat.SetColor("_Color", Color.Lerp(_cardMat.GetColor("_Color"), new Color(1, 1, 1, 1f), t));

            t += Time.deltaTime * _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        _on = true;
        if (_btn)
            _btn.interactable = true;
        objRotate.Instance.rotating = true;
    }

    IEnumerator FadeCardOut()
    {
        Color _lineStart = _line.GetColor("_BaseColor");
        if (_btn)
            _btn.interactable = false;
        float t = 0;
        while (t < 1)
        {
            _line.SetColor("_BaseColor", Color.Lerp(_line.GetColor("_BaseColor"), new Color(_lineStart.r, _lineStart.g, _lineStart.b, 0f), t));
            _cardMat.SetColor("_Color", Color.Lerp(_cardMat.GetColor("_Color"), new Color(1, 1, 1, 0f), t));

            t += Time.deltaTime * _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        _on = false;
        if (_btn)
            _btn.interactable = true;
    }

    IEnumerator InitSpawnedObj()
    {
        while (_card == null)
        {
            if (GameObject.FindGameObjectWithTag(CardToLink.ToString()) != null)
            {
                _card = GameObject.FindGameObjectWithTag(CardToLink.ToString());
                _line = _card.GetComponent<LineRenderer>().material;
                _cardMat = _card.transform.GetChild(0).GetComponent<SpriteRenderer>().material;
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    public enum Cards { Deforestation, Wildfires, Tornados, SeaLevels, Earthquakes, Heatwaves, OzoneCard, Volcanos, Tsunamis };
    public enum Option { Button, Marker };
}
