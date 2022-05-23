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

        transform.DOMoveY(startPosY, 0.25f); // Spawn animation
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.GetIsGameActive())
        {
            if (!moved)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        myTween = transform.DOMove(Vector3.forward, 1f);
        SpawnManager.Instance.SpawnArrow();
        moved = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        myTween.Kill();
        
        if (!collided && collision.gameObject.tag != null)
        {
            ProcessArrowCollision(collision);
        }
    }

    private void ProcessArrowCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameManager.Instance.SetGameActive(false);
            transform.SetParent(collision.gameObject.transform);

            myTween.Kill();
            rb.isKinematic = true;
            arrow.enabled = false;

            MakeCollidedArrowColorsRed(collision.gameObject);

            Cube.Instance.DoFailMove();
        }

        collision.gameObject.GetComponent<Quad>().ProcessQuadCollision(gameObject);
        collided = true;
        rb.isKinematic = true;
        arrow.enabled = false;
    }

    private void MakeCollidedArrowColorsRed(GameObject collision)
    {
        if (!Cube.Instance.isWinMoveDone)
        {
            GameObject arrowChild = gameObject.transform.GetChild(0).gameObject;
            GameObject collisionChild = collision.transform.GetChild(0).gameObject;

            gameObject.GetComponent<Renderer>().material.color = Color.black;
            arrowChild.GetComponentInChildren<Renderer>().material.color = Color.red;

            collision.GetComponent<Renderer>().material.color = Color.black;
            collisionChild.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }
}