using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : Singleton<CubeController>
{
    [SerializeField] private List<GameObject> quads;

    private Tween rotation;
    private int scoreCounter; // the number of quads that have 0 on 
    
    void Start()
    {
        Rotate();
    }

    void Rotate()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
             rotation = transform.DORotate(Vector3.up * 360, 5, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }

    public void DoFailMove()
    {
        rotation.Kill();
        transform.DOShakePosition(0.5f, 0.50f, 25);
        
        // Medium vibration here
    }
    
    public void DoWinMove()
    {
        rotation.Kill();
        transform.DORotate(Vector3.up * -360, 2, RotateMode.FastBeyond360);
        transform.DOJump(transform.position, 1.2f, 3, 1.5f);
        
        // Small Vibration here
    }

    private void CheckScores()
    {
        if (scoreCounter == 4)
        {
            GameManager.Instance.SetGameActive(false);

            DoWinMove();
        }
    }

    public void IncreaseScoreCounter()
    {
        scoreCounter++;
        CheckScores();
    }
    
}