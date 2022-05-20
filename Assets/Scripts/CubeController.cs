using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : Singleton<CubeController>
{
    [SerializeField] private List<GameObject> quads;

    private Tween rotation;
    private int scoreCounter; // the number of quads that have 0 on 
    private float rotationSpeed = 2f;
    
    void Start()
    {
        Rotate();
    }

    void Rotate()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
             rotation = transform.DORotate(Vector3.up * 360 * rotationSpeed, 5, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }

    public void DoFailMove()
    {
        rotation.Kill();
        transform.DOShakePosition(0.5f, 0.50f, 25);
        
        // Medium vibration here
    }
    
    public IEnumerator DoWinMove()
    {
        rotation.Kill();
        yield return new WaitForSeconds(0.1f);
        transform.DORotate(Vector3.up * -360, 2f, RotateMode.FastBeyond360);
        transform.DOJump(transform.position, 1.2f, 3, 1.5f);
        
        // Small Vibration here
    }

    private void CheckScores()
    {
        if (scoreCounter == 4)
        {
            GameManager.Instance.SetGameActive(false);

            StartCoroutine("DoWinMove");
        }
    }

    public void IncreaseScoreCounter()
    {
        scoreCounter++;
        CheckScores();
    }
    
}