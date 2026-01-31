using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attack : MonoBehaviour
{


    public HealthSystem _healthSystem;
    [SerializeField] bool _attackController;
    [SerializeField] float _attackDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _attackController = true;
            AttackHand();
            
        }
    }
    public void AttackHand()
    {
        if (_attackController)
        {
            _healthSystem.HealthDecrease(_attackDamage);
            Debug.Log("AAAA");
            _attackController = false;
        }

    }
}
