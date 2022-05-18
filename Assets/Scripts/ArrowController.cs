using DG.Tweening;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private bool moved;
    private Tween myTween;

    private Rigidbody rb;
    private ArrowController arrowController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrowController = GetComponent<ArrowController>();
        moved = false;

        transform.DOMoveY(-0.4f, 0.25f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!moved)
            {
                myTween = transform.DOMove(Vector3.forward, 1f);
                moved = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            myTween.Kill();
            rb.isKinematic = true;
            arrowController.enabled = false;
        }
    }
}