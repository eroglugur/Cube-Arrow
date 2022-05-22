using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : Singleton<Cube>
{
    [SerializeField] private List<GameObject> quads;

    private Tween rotation;
    private int scoreCounter;
    private float rotationSpeed = 3f;

    private bool isFailMoveDone;

    void Start()
    {
        Rotate();
        isFailMoveDone = false;
    }

    void Rotate()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
            rotation = transform.DORotate(Vector3.up * 360, rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear).SetLoops(-1);
        }
    }

    public void DoFailMove()
    {
        if (!isFailMoveDone)
        {
            rotation.Kill();

            transform.DOShakePosition(0.5f, 0.50f, 25, 0);
            isFailMoveDone = true;

            // Medium vibration here
        }
    }

    public IEnumerator DoWinMove()
    {
        rotation.Kill();
        yield return new WaitForSeconds(0.1f);
        transform.DORotate(Vector3.up * -360, 2f, RotateMode.FastBeyond360);
        transform.DOJump(transform.position, 1.2f, 3, 1.5f);

        // Small Vibration here
    }
}