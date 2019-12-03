using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnTilt : MonoBehaviour
{
    //References to primitive fields
    private float shakeDuration = 0f;
    private float shakeMagnitudeMax = .75f;
    private float shakeMagnitudeCurrent = 0f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Count down duration, shaking a random amount each time
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitudeCurrent;

            shakeDuration -= Time.deltaTime;
            shakeMagnitudeCurrent -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void ShakeForDuration(float duration)
    {
        shakeDuration = duration;
        shakeMagnitudeCurrent = shakeMagnitudeMax;
    }
}
