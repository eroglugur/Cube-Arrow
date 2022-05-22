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
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.GetIsGameActive())
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
        if (!collision.gameObject.CompareTag(null) && !collided)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                GameManager.Instance.SetGameActive(false);

                transform.SetParent(collision.gameObject.transform);
                
                myTween.Kill();
                rb.isKinematic = true;
                arrow.enabled = false;

                Cube.Instance.DoFailMove();
            }

            collision.gameObject.GetComponent<Quad>().ProcessCollision(gameObject);

            collided = true;
            myTween.Kill();
            rb.isKinematic = true;
            arrow.enabled = false;
        }
    }
}