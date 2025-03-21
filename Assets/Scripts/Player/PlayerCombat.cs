using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Controls controls;
    private CharacterController playerCC;
    private Animator playerAnimator;
    private bool canAttack = true;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        playerCC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Attack.triggered && canAttack)
        {
            canAttack = false;
            StartCoroutine(nameof(AttackCD));
            playerAnimator.Play("MeeleeAttack_OneHanded");
        }
    }
    public IEnumerator AttackCD()
    {
        while (!canAttack)
        {
            yield return new WaitForSeconds(2);
            canAttack = true;
            StopCoroutine(nameof(AttackCD));
        }
    }
}
