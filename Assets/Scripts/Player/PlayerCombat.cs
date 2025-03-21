using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerMove movementScript;
    private Animator swordAnimator;
    private bool canAttack = true;

    private void OnEnable()
    {
        swordAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (movementScript.controls.Player.Attack.triggered && canAttack)
        {
            canAttack = false;
            StartCoroutine(nameof(AttackCD));
            swordAnimator.Play("SwordSpin");
        }
    }

    public IEnumerator AttackCD()
    {
        while (!canAttack)
        {
            yield return new WaitForSeconds(1);
            canAttack = true;
            StopCoroutine(nameof(AttackCD));
            swordAnimator.Play("SwordIdle");
        }
    }
}
