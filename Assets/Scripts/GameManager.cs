using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string percent10_50;
    [SerializeField] private string percent50_80;
    [SerializeField] private string percent80_100;
    [SerializeField] private float MaxDeadLineTime;

    public enum StateGameOver
    {
        trash,
        ok,
        amazing
    }

    private ScreenColorGame screenColorGame;
    private Creater[] creaters;
    private DeadLine deadLine;
    private BlindTrigger blindTrigger;
    private FlashLight flashLight;
    private PlayerController playerController;
    private CharacterAiming aiming;
    private UiController uiController;
    private SpawnIdeasSystem spawnIdeasSystem;


    private void Awake()
    {
        screenColorGame = FindObjectOfType<ScreenColorGame>();
        creaters = FindObjectsOfType<Creater>();
        deadLine = FindObjectOfType<DeadLine>();
        blindTrigger = FindObjectOfType<BlindTrigger>();
        flashLight = FindObjectOfType<FlashLight>();
        var cinemachine = FindObjectOfType<CinemachineFreeLook>();
        playerController = FindObjectOfType<PlayerController>();
        playerController.cinemachine = cinemachine;
        uiController = FindObjectOfType<UiController>();
        spawnIdeasSystem = FindObjectOfType<SpawnIdeasSystem>();
        Subscrible();
    }

    private void Subscrible()
    {
        deadLine.MAXTime = MaxDeadLineTime;
        deadLine.OnTimeDeadLine += () => uiController.SetOver(true);
        deadLine.OnTimeDeadLine += DedlineIsOwer;
        if (blindTrigger is { }) blindTrigger.flashlight = flashLight.gameObject;
        if (flashLight is { }) flashLight.gameObject.SetActive(false);
        screenColorGame.OnPercentGame += SetStatGameOver;
        screenColorGame.OnColorGame += uiController.SetColorFinalGame;
        screenColorGame.OnColorTargetGame += uiController.SetColorTargetGame;
        foreach (var creater in creaters)
        {
            creater.OnColor += screenColorGame.SetColor;
        }
        
        playerController.isActive = true;
    }

    private void DedlineIsOwer()
    {
        aiming.isActive = false;
        playerController.isActive = false;
        deadLine.OnTimeDeadLine -= DedlineIsOwer;
        foreach (var creater in creaters)
        {
            creater.OnColor -= screenColorGame.SetColor;
        }

        deadLine.OnTimeDeadLine -= () => uiController.SetOver(true);

        SetCursor();
    }

    private void SetStatGameOver(float f)
    {
        string text = "";
        uiController.SetPercent(f.ToString("0.0"));
        if (f < 50)
        {
            text = percent10_50;
        }
        else if (f > 50 && f < 80)
        {
            text = percent50_80;
        }
        else if (f > 80)
        {
            text = percent80_100;
        }

        uiController.SetTextCongratulation(text);
    }

    private void SetCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}