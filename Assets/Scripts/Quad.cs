using TMPro;
using UnityEngine;

public class Quad : MonoBehaviour
{
    private TMP_Text scoreText;
    public int quadScore;

    void Awake()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
        SetTextColor(Color.yellow);
        
        quadScore = int.Parse(scoreText.text);
        RandomizeQuadNumber();
    }

    public void ProcessCollision(GameObject collision)
    {
        UpdateQuadScore();
        
        collision.gameObject.transform.SetParent(transform);
    }

    private void UpdateQuadScore()
    {
        if (quadScore > 0)
        {
            quadScore--;
            
            UpdateQuadScoreText();
        }
        else
        {
            quadScore--;

            GameManager.Instance.SetGameActive(false);
            Cube.Instance.DoFailMove();
        }

        if (quadScore == 0)
        {
            SetTextColor(Color.green);
            GameManager.Instance.IncreaseScoreCounter();
        }
        else if (quadScore < 0)
        {
            SetTextColor(Color.red);
        }
    }

    private void SetTextColor(Color color)
    {
        scoreText.color = color;
    }
    
    private void UpdateQuadScoreText()
    {
        scoreText.text = quadScore.ToString();
    }

    public void RandomizeQuadNumber()
    {       
        scoreText = GetComponentInChildren<TMP_Text>();
        quadScore = int.Parse(scoreText.text);

        int randomNumber = Random.Range(1, 6);
        quadScore = randomNumber;
        scoreText.text = quadScore.ToString();
    }
    
}