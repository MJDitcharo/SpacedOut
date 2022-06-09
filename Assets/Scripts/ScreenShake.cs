using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator ShakeScreen(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float originalMagnitude = magnitude;

        float timeElapsed = 0;
        Debug.Log(duration);

        while (timeElapsed < duration)
        {
            float shakeX = Random.Range(-1f, 1f) * magnitude;
            float shakeY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(shakeX, shakeY, transform.localPosition.z);

            timeElapsed += Time.deltaTime;

            yield return null;

            magnitude = Mathf.Lerp(originalMagnitude, 0, timeElapsed / duration);
        }

        transform.localPosition = originalPosition;
    }
}
