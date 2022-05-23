
public class GameManager : Singleton<GameManager>
{
    private bool isGameActive;

    private int scoreCounter; // the number of quads that have 0 on 
    
    void Awake()
    {
        isGameActive = true;
    }

    private void CheckScores()
    {
        if (scoreCounter == 4)
        {
            StartCoroutine(Cube.Instance.DoWinMove());
            SetGameActive(false);
        }
    }

    public void IncreaseScoreCounter()
    {
        scoreCounter++;
        CheckScores();
    }
    public bool GetIsGameActive()
    {
        return isGameActive;
    }

    public void SetGameActive(bool isActive)
    {
        isGameActive = isActive;
    }
}
