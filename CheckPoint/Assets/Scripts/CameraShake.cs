using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // This method starts the coroutine with the given duration and magnitude
    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    // The coroutine for shaking the camera
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        transform.localPosition = originalPos; // Reset position after shaking
    }
}