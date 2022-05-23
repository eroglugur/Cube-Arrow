using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Cube : Singleton<Cube>
{
    [SerializeField] private List<GameObject> quads;

    private Tween rotation;
    private float rotationSpeed = 3f;

    public bool isFailMoveDone;
    public bool isWinMoveDone;

    void Start()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
            Rotate();
        }

        isFailMoveDone = false;
        isWinMoveDone = false;
    }

    void Rotate()
    {
        rotation = transform.DORotate(Vector3.up * 360, rotationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }

    public void DoFailMove()
    {
        if (isFailMoveDone || isWinMoveDone) return;
        rotation.Kill();

        transform.DOShakePosition(0.5f, 0.50f, 25, 0);

        SetQuadColorsRed();
        isFailMoveDone = true;

        // Medium vibration here
    }

    public IEnumerator DoWinMove()
    {
        if (!isWinMoveDone && !isFailMoveDone)
        {
            isWinMoveDone = true;

            rotation.Kill();
            yield return new WaitForSeconds(0.1f);
            transform.DORotate(Vector3.up * -360, 2f, RotateMode.FastBeyond360);
            transform.DOJump(transform.position, 1.2f, 3, 1.5f);
        }

        // Small Vibration here
    }
    
    private void SetQuadColorsRed()
    {
        if (!isWinMoveDone)
        {
            foreach (var quad in quads)
            {
                quad.GetComponentInChildren<TMP_Text>().color = Color.red;
            }
        }
    }

}