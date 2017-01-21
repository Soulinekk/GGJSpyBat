using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration =1f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 1f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void Shake(float t,float a,float g)
    {
        shakeDuration = t;
        shakeAmount = a;
        decreaseFactor = g;
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y,-50f);
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y, -50f);
        }
    }
}

