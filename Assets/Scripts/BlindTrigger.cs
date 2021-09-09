using System.Collections;
using UnityEngine;

public class BlindTrigger : MonoBehaviour
{
    public float blindingSpeed = 0.03f;

    private void OnTriggerExit(Collider other)
    {
        var temp = other.GetComponent<PlayerController>();
        if (temp != null)
        {
            bool doBlind = transform.InverseTransformPoint(other.transform.position).z > 0;
            Debug.Log(doBlind);
            Blind(doBlind, blindingSpeed);
        }
    }

    public void Blind(bool value, float speed)
    {
        StopAllCoroutines();
        if (value)
        {
            StartCoroutine(IEBlinding(0f, speed));
        }
        else
        {
            StartCoroutine(IEBlinding(1f, speed));
        }
    }

    IEnumerator IEBlinding(float targetIntensity, float speed)
    {
        float startIntesity = RenderSettings.ambientIntensity;
        float timer = Mathf.Abs(targetIntensity - startIntesity);
        while (RenderSettings.ambientIntensity != targetIntensity)
        {
            yield return new WaitForSeconds(0.02f);
            timer -= speed;
            float intensity = Mathf.Lerp(startIntesity, targetIntensity, 1f - timer);
            RenderSettings.ambientIntensity = intensity;
            RenderSettings.reflectionIntensity = intensity;
        }

        flashlight.gameObject.SetActive(targetIntensity == 0);
    }

    public GameObject flashlight { get; set; }
}