using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScreenColorGame : MonoBehaviour
{
    public event Action<float> OnPercentGame;
    public event Action<Color> OnColorGame;
    public event Action<Color> OnColorTargetGame;
    [SerializeField] private Image screen;
    [SerializeField] private Image targetGameScreen;
    [SerializeField] private Text UiPercentRelation;
    private Color targetGame;
    private float red;
    private float green;
    private float blue;


    private void Start()
    {
        targetGameScreen.color = new Color(GetRandom(), GetRandom(), GetRandom());
        OnColorTargetGame?.Invoke(targetGameScreen.color);
        OnPercentGame?.Invoke(0);
        UiPercentRelation.text = "0.0";
        //Relation();
    }

    private float GetRandom()
    {
        return Random.Range(0f, 1f);
    }

    public void SetColor(Idea.ColorIdea colorIdea, float count)
    {
        switch (colorIdea)
        {
            case Idea.ColorIdea.red:
                red = count;
                break;
            case Idea.ColorIdea.green:
                green = count;
                break;
            case Idea.ColorIdea.blue:
                blue = count;
                break;
        }

        screen.color = new Color(red / 255, green / 255, blue / 255);
        Relation();
    }

    private void Relation()
    {
        var rColor = Mathf.Abs(screen.color.r - targetGameScreen.color.r);
        var gColor = Mathf.Abs(screen.color.g - targetGameScreen.color.g);
        var bColor = Mathf.Abs(screen.color.b - targetGameScreen.color.b);
        var sum = rColor + gColor + bColor;
        var del = sum / 3;
        var itog = (1 - del) * 100;
        UiPercentRelation.text = itog.ToString("0.0");
        OnPercentGame?.Invoke(itog);
        OnColorGame?.Invoke(screen.color);
    }
}