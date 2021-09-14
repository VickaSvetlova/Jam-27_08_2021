using Cinemachine;
using DefaultNamespace;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IIsActive
{
    [SerializeField] private Transform pointIdea;
    private Idea inArm;
    private Animator animator;
    private Rigidbody rigidbody;
    private float horizontal, vertical;
    [HideInInspector] public CinemachineFreeLook cinemachine;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject objectForHide;
    [SerializeField] private GameObject charForHide;
    [SerializeField] private Transform cameraPoint;

    [SerializeField] private float turnSpeed = 15;
    private Transform cameraTransform;
    private Vector3 defaultScale;

    public Idea InArm
    {
        get => inArm;
        set => inArm = value;
    }

    public bool isHidden { get; private set; }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        defaultScale = transform.localScale;
    }

    public void SetHidden(bool value) {
        isHidden = value;
        if (inArm) {
            var renders = inArm.GetComponentsInChildren<Renderer>();
            foreach (var render in renders) {
                render.enabled = !value;
            }
        }
        if (value) {
            StopAllCoroutines();
            StartCoroutine(IEHide());
        } else {
            StopAllCoroutines();
            StartCoroutine(IEShow());
        }
    }

    IEnumerator IEShow() {
        cinemachine.LookAt = cameraPoint;
        float timer = 0;
        Vector3 end = new Vector3(.3f, .3f, .3f);
        objectForHide.SetActive(false);
        transform.localScale = end;
        charForHide.SetActive(true);
        while (timer < 1f) {
            timer += 5f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(end, defaultScale * 1.3f, timer);
            yield return null;
        }
        timer = 0;
        while (timer < 1f) {
            timer += 5f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(defaultScale * 1.3f, defaultScale, timer);
            yield return null;
        }
        transform.localScale = defaultScale;
    }

    IEnumerator IEHide() {
        float timer = 0;
        Vector3 end = new Vector3(.3f, .3f, .3f);
        cinemachine.LookAt = transform;
        while (timer < 1f) {
            timer += 5f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(defaultScale, end, timer);
            yield return null;
        }
        objectForHide.SetActive(true);
        charForHide.SetActive(false);
        timer = 0;
        while (timer < 1f) {
            timer += 6f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(end, defaultScale * 1.3f, timer);
            yield return null;
        }
        timer = 0;
        while (timer < 1f) {
            timer += 12f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(defaultScale * 1.3f, defaultScale, timer);
            yield return null;
        }
        transform.localScale = defaultScale;
    }

    private void Update()
    {
        if (isActive) {
            if (Input.GetButtonDown("Jump")) {
                SetHidden(!isHidden);
            }
            if (!isHidden) {
                var h = Input.GetAxis("Horizontal");
                var v = Input.GetAxis("Vertical");
                horizontal = Mathf.Lerp(horizontal, h, 10f * Time.deltaTime);
                vertical = Mathf.Lerp(vertical, v, 10f * Time.deltaTime);
                if (h != 0 || v != 0) {
                    float yawCamera = cameraTransform.eulerAngles.y;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0),
                        turnSpeed * Time.deltaTime);
                }
                return;
            }
        }
        horizontal = 0;
        vertical = 0;
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