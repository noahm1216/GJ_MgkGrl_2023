using UnityEngine;
using System.Collections;

public class Fade_material : MonoBehaviour {

    public static bool fadeDark = false;
    public static bool fadeLight = false;

    [SerializeField] private bool loopFading = true;
    [SerializeField] private float speedOffset = 1;
    [SerializeField] private float waitOffset = 0.5f;

    private Color currentColor;

    void Start()
    {

        //StartCoroutine(FadeTo(0.0f, 1.0f)); // when the game starts be at zero alpha
        fadeLight = true;
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
            StartCoroutine(FadeTo(1.0f, 0.0f));
            fadeDark = false;
        }


    }


    IEnumerator FadeTo(float aValue, float aTime)
    {
        
        if(currentColor == null) currentColor = transform.GetComponent<SpriteRenderer>().material.color;
        float alpha = currentColor.a;

        yield return new WaitForSeconds(waitOffset);

        for (float t = 0.0f; t < 1.0f; t += (Time.deltaTime / aTime) * speedOffset)
        {
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(alpha, aValue, t));

            currentColor = newColor;
            yield return null;
        }

        if (alpha <= 0 && loopFading)
            fadeLight = true;
        if (alpha > 0.99f && loopFading)
            fadeDark = true;

    }
}
