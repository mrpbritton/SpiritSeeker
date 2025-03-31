using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public bool isActive;
    public float lifetime;
    public float damage = -10;
    public List<MeshRenderer> boltPieces = new List<MeshRenderer>();

    private Vector3 firingDirection;
    private float firingSpeed;
    private float timer;
    private BoxCollider boltCollider;

    private void OnEnable()
    {
        boltCollider = GetComponent<BoxCollider>();
        timer = 0;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            // Debug.Log(this.gameObject.name + "is active and firing");
            transform.position += firingDirection * Time.fixedDeltaTime * firingSpeed;

            timer += Time.fixedDeltaTime;
            if(timer >= lifetime)
            {
                Deactivate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HPController>(out HPController player))
        {
            player.UpdateHealth(damage);
        }

        Deactivate();
    }

    public void Fire(float setSpeed, Vector3 setDirection)
    {
        Debug.Log("Fire called");

        foreach (var piece in boltPieces)
        {
            piece.enabled = true;
        }
        boltCollider.enabled = true;
        isActive = true;

        firingDirection = setDirection;
        firingSpeed = setSpeed;
    }

    public void Deactivate()
    {
        foreach(var piece in boltPieces)
        {
            piece.enabled = false;
        }
        boltCollider.enabled = false;
        isActive = false;
        timer = 0;
    }
}
