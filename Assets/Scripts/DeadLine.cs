using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeadLine : MonoBehaviour
{
    public event Action OnTimeDeadLine;
    public Color StartColor;
    public Color EndColor;
    private Color Color;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image UIBar;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float maxTime;
    [SerializeField] private Vector2 starScale;
    [SerializeField] private Vector2 endScale;

    private Camera camera;
    private RectTransform rect;

    public float MAXTime
    {
        get => maxTime;
        set => maxTime = value;
    }


    private void Start()
    {
        camera = Camera.main;
        StartCoroutine("TimeLineColor");
        rect = canvas.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (playerPos == null) return;
        var pos = playerPos.position;
        pos.y = transform.position.y;
        transform.position = pos + playerPos.forward * offsetPosition.z;
        transform.forward = camera.transform.forward;
    }

    IEnumerator TimeLineColor()
    {
        float currentTime = MAXTime;
        while (currentTime > 0)
        {
            yield return null;
            currentTime -= Time.deltaTime;
            var time = currentTime / MAXTime;
            UIBar.color = Color.Lerp(EndColor, StartColor, time);
            UIBar.fillAmount = time;
            transform.localScale = (Vector3.Lerp(endScale, starScale, time));
        }

        OnTimeDeadLine?.Invoke();
    }
}