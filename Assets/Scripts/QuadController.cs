using TMPro;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    private TMP_Text scoreText;
    public int score;

    void Awake()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
        SetTextColor(Color.yellow);
        
        score = int.Parse(scoreText.text);
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateScore();
        
        collision.gameObject.transform.SetParent(transform);
        ArrowSpawner.Instance.SpawnArrow();
    }
    

    private void UpdateScore()
    {
        if (score > 0)
        {
            score--;
            
            UpdateScoreText();
        }
        else
        {
            score--;

            GameManager.Instance.SetGameActive(false);
            CubeController.Instance.DoFailMove();
        }

        if (score == 0)
        {
            SetTextColor(Color.green);
            CubeController.Instance.IncreaseScoreCounter();
        }
        else if (score < 0)
        {
            SetTextColor(Color.red);
        }
    }

    private void SetTextColor(Color color)
    {
        scoreText.color = color;
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    
}