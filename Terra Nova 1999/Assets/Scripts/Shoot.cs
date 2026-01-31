using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour,IInteractable
{
    public HealthSystem _healthSystem;

    [SerializeField] float _shootDamage;

    public void Interact()
    {
        Shooting();
    }

    public void Shooting() 
    {
        _healthSystem.HealthDecrease(_shootDamage);

    }
    private void Update()
    {

    }
}
