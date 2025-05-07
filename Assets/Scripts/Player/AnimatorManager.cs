using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            animator.enabled = false;
        }
        else if (Time.timeScale == 1 && animator.enabled == false)
        {
            animator.enabled = true;
        }
    }
}
