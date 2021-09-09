using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject UIIsOver;
    [SerializeField] private Text UIIsOverPercent;
    [SerializeField] private Text UICongratText;
    [SerializeField] private Image UIIsOverColor;
    [SerializeField] private Image UIIsTargetColor;

    public void SetOver(bool state)
    {
        UIIsOver.SetActive(state);
    }

    public void SetPercent(string toString)
    {
        UIIsOverPercent.text = toString;
    }

    public void SetColorFinalGame(Color color)
    {
        UIIsOverColor.color = color;
    }

    public void SetColorTargetGame(Color color)
    {
        UIIsTargetColor.color = color;
    }

    public void SetTextCongratulation(string text)
    {
        UICongratText.text = text;
    }
}