using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade_material : MonoBehaviour {

    public  bool fadeDark = false;
    public  bool fadeLight = false;

    public bool usesSpriteRenderer;

    void Start()
    {
        if (fadeLight == true)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
            fadeLight = false;
        }

        if (fadeDark == true)
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
            fadeDark = false;
        }
    }


    void Update()
    {

        if (fadeLight== true)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
            fadeLight = false;
        }

        if (fadeDark == true)
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
            fadeDark = false;
        }


    }


    IEnumerator FadeTo(float aValue, float aTime)
    {
        if (usesSpriteRenderer)
        {
            float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;

            yield return new WaitForSeconds(0.5f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(alpha, aValue, t));
                transform.GetComponent<SpriteRenderer>().material.color = newColor;
                yield return null;
            }
        }
        else
        {
            float alpha = transform.GetComponent<Image>().color.a;

            yield return new WaitForSeconds(0.5f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(alpha, aValue, t));
                transform.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
        

    }
}
