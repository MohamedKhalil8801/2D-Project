using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve Curve;

    private Vector3 startPos;

    private void OnEnable() {
        startPos = transform.position;
    }

    public void ShakeCamera(float magnitude, float duration){
        StartCoroutine(Shake(magnitude, duration));
    }

    private IEnumerator Shake(float magnitude, float duration){
        startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float strength = Curve.Evaluate(elapsedTime/duration) * magnitude;
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPos;
    }
}
