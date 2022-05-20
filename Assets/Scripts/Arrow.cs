using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool moved;
    private bool collided;
    private Tween myTween;

    private Rigidbody rb;
    private Arrow arrow;

    private float startPosY = -0.4f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow = GetComponent<Arrow>();
        moved = false;
        collided = false;

        transform.DOMoveY(startPosY, 0.25f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!moved)
            {
                myTween = transform.DOMove(Vector3.forward, 1f);
                SpawnManager.Instance.SpawnArrow();
                moved = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !collided)
        {
            collided = true;
            collision.gameObject.GetComponent<Quad>().ProcessCollision(gameObject);
            
            myTween.Kill();
            rb.isKinematic = true;
            arrow.enabled = false;
            
        }
    }
}