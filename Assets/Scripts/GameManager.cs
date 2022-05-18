
public class GameManager : Singleton<GameManager>
{
    private bool isGameActive; 
    
    void Awake()
    {
        isGameActive = true;
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
