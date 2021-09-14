using System;
using System.Collections;
using UnityEngine;

public class Idea : MonoBehaviour
{
    public event Action<Idea> OnDestroyIdea;

    public enum ColorIdea
    {
        red,
        green,
        blue
    }

    [SerializeField] private ColorIdea colorIdea;
    [SerializeField] private Renderer renderer;
    [SerializeField] private float forceDebuf = 2f;
    [SerializeField] private float maxDebaf = 10f;
    [SerializeField] private float idleDebaf = 0.1f;
    private float currentTimeIdea;
    [HideInInspector] public float timeLifeIdea;
    private IEnumerator timer;
    private SphereCollider _collider;

    private float debaf = 0;

    [Header("Shader settings")]
    [SerializeField] private Vector2 fillAmountMinMax;

    public void AddDebuf(int debafCount)
    {
        debaf = Mathf.Clamp(debaf + debafCount * forceDebuf, 0, maxDebaf);
        //  Debug.Log("debaf " + this.debaf);
    }

    public ColorIdea Color_Idea
    {
        get => colorIdea;
        set => colorIdea = value;
    }

    public float CurrentTimeIdea
    {
        get => currentTimeIdea;
        set => currentTimeIdea = value;
    }

    private void Start()
    {
        SetColor(Color_Idea);
        _collider = GetComponent<SphereCollider>();
    }

    public void TakeIdea()
    {
        StartCoroutine(Timer());
        _collider.enabled = false;
    }

    private void SetColor(ColorIdea color) {
        Color c = Color.white;
        switch (color)
        {
            case ColorIdea.red: c = Color.red; break;
            case ColorIdea.green: c = Color.green; break;
            case ColorIdea.blue: c = Color.blue; break;
        }
        renderer.materials[0].SetColor("_Tint", c);
        renderer.materials[0].SetColor("_TopColor", c);
    }

    IEnumerator Timer()
    {
        currentTimeIdea = timeLifeIdea;
        while (currentTimeIdea > 0)
        {
            yield return null;
            currentTimeIdea -= Time.deltaTime * (debaf + idleDebaf);
            float fillAmount = Mathf.Lerp(fillAmountMinMax.x, fillAmountMinMax.y, currentTimeIdea / timeLifeIdea);
            renderer.materials[0].SetFloat("_FillAmount", fillAmount);
            Debug.Log("saturation " + currentTimeIdea + " debuf " + (debaf + idleDebaf));
        }

        OnDestroyIdea?.Invoke(this);
    }
    
}