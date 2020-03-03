using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenFade : MonoBehaviour
{
    [SerializeField]
    float time;

    private void Start()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(time);
        float t = 1f;
        float fadeSpeed = 2f;

        while (t > 0)
        {
            foreach(Image img in GetComponentsInChildren<Image>())
                img.color = new Color(img.color.r, img.color.g, img.color.b, t);

            foreach(Text txt in GetComponentsInChildren<Text>())
                txt.color= new Color(txt.color.r, txt.color.g, txt.color.b, t);

            t -= Time.deltaTime / fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}