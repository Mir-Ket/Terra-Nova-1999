using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAttacksEvents : MonoBehaviour
{
    public Attack attack;
    [SerializeField] GameObject _leftAttack;
    [SerializeField] GameObject _rightAttack;
    [SerializeField] GameObject _bigAttack;
    [SerializeField] GameObject _epicAttack;

    [SerializeField] GameObject _particles;

    [SerializeField] AudioSource _attack1;
    [SerializeField] AudioSource _attack2;
    [SerializeField] AudioSource _attack3;
    [SerializeField] AudioSource _attack4;

    [SerializeField] Animator _anim;


    public void LeftHandenable()
    {
        attack.AttackHand();
        _leftAttack.SetActive(true);
        _attack1.Play();
        
        
        
    }
    public void LeftHandDisable()
    {
        _leftAttack.SetActive(false);

    }
    public void RightHandenable()
    {
        attack.AttackHand();
        _rightAttack.SetActive(true);
        _attack2.Play();


    }
    public void RgihtHandDisable()
    {
        _rightAttack.SetActive(false);
        
    }
    public void BigHandenable()
    {
        attack.AttackHand();
        _bigAttack.SetActive(true);
       
        _attack3.Play();
        _anim.enabled = true;
    

    }
    public void BigHandDisable()
    {
        _bigAttack.SetActive(false);
        _anim.enabled = false;


    }
    public void EpicAttackdenable()
    {
        attack.AttackHand();
        _epicAttack.SetActive(true);
        _anim.enabled = true;

        _attack4.Play();


    }
    public void EpicAttackDisable()
    {
        _epicAttack.SetActive(false);

        Invoke(nameof(Delayer), 2f);
        
    }

    private void Delayer()
    {
        _anim.enabled = false;
    }
    public void Vfxdenable()
    {
    _particles.SetActive(true);


    }
    public void VfxkDisable()
    {
        _particles.SetActive(false);


    }

}
