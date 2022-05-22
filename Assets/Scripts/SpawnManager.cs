using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<GameObject> quads;
    [SerializeField] private GameObject arrow;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        SpawnArrow();
    }

    private int GetArrowCount()
    {
        foreach (var quad in quads)
        {
            count += quad.GetComponentInChildren<Quad>().quadScore;
        }

        return count;
    }

    public void SpawnArrow()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
            if (GetArrowCount() > 0)
            {
                Instantiate(arrow);
                count--;
            }
            else if (count < 0)
            {
                Cube.Instance.DoFailMove();
            }
        }
    }
}