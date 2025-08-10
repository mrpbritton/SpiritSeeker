using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject northWall;
    [SerializeField] private GameObject southWall;
    [SerializeField] private GameObject eastWall;
    [SerializeField] private GameObject westWall;
    [SerializeField] private GameObject fill;

    public bool isVisited { get; private set; }

    public void Visit()
    {
        isVisited = true;
        fill.SetActive(false);
    }

    public void DisableNorthWall()
    {
        northWall.SetActive(false);
    }

    public void DisableSouthWall()
    {
        southWall.SetActive(false);
    }

    public void DisableEastWall()
    {
        eastWall.SetActive(false);
    }

    public void DisableWestWall()
    {
        westWall.SetActive(false);
    }
}
