using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour, IIsActive
{
    [SerializeField] private Transform pointIdea;
    private Idea inArm;
    private Animator animator;
    private Rigidbody rigidbody;
    private float horizontal, vertical;
    [SerializeField] private GameObject flashlight;

    public Idea InArm
    {
        get => inArm;
        set => inArm = value;
    }


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isActive)
        {
            horizontal = Mathf.Lerp(horizontal, Input.GetAxis("Horizontal"), 10f * Time.fixedDeltaTime);
            vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical"), 10f * Time.fixedDeltaTime);
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("InputX", horizontal);
        animator.SetFloat("InputY", vertical);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tempIdea = other.GetComponent<Idea>();
        var tempCreater = other.GetComponent<Creater>();
        if (tempIdea != null && InArm == null)
        {
            InArm = tempIdea;
            tempIdea.TakeIdea();
            tempIdea.gameObject.transform.position = pointIdea.position;
            tempIdea.gameObject.transform.SetParent(pointIdea);
            tempIdea.OnDestroyIdea += DestroyedIdea;
        }

        if (tempCreater != null && InArm != null)
        {
            tempCreater.TakeColor(InArm.Color_Idea, InArm.CurrentTimeIdea);
            var idea = InArm.gameObject;
            inArm.OnDestroyIdea -= DestroyedIdea;
            InArm = null;
            Destroy(idea);
        }
    }

    public void DestroyedIdea(Idea idea)
    {
        idea.OnDestroyIdea -= DestroyedIdea;
        InArm = null;
        Destroy(idea.gameObject);
    }

    public bool isActive { get; set; }
}