using System.Collections;
using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    private GameObject objective;
    private Vector3 lookDirection;
    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        if(GameObject.FindWithTag("Goal") != null)
        {
            objective = GameObject.FindWithTag("Goal");
        }
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(objective != null)
        {
            lookDirection = objective.transform.position - transform.position;
            lookDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            if (GameObject.FindWithTag("Goal") != null)
            {
                objective = GameObject.FindWithTag("Goal");
            }
        }
    }

    public void makeArrowVisible()
    {
        meshRenderer.enabled = true;
    }

    public void makeArrowInvisible()
    {
        meshRenderer.enabled = false;
    }
}
