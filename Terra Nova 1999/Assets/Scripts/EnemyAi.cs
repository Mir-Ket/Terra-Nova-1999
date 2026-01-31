using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float _attackRange;
    [SerializeField] float _detectRange;
    [SerializeField] float _attackSpeed = 1.5f; // Saldýrýlar arasý bekleme süresi
    [SerializeField] LayerMask _playerLayerMask;

    [SerializeField] Animator _anim;
    [SerializeField] Transform _target;
   

    // Bu deðiþken saldýrý sýrasýnda tekrar emir verilmesini engeller
    private bool _isAttacking = false;

    

    void Update()
    {


        // 1. Önce oyuncu var mý kontrol edelim
        // (Performans için OverlapSphere yerine basit mesafe kontrolü daha iyidir ama senin yapýný korudum)
        bool playerDetected = Physics.CheckSphere(transform.position, _detectRange, _playerLayerMask);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayerMask);

        if (playerDetected)
        {
            // Oyuncuya dön
            Vector3 LookPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.LookAt(LookPos);

            // Eðer saldýrý menzilindeyse VE þu an saldýrmýyorsa
            if (playerInAttackRange)
            {
                _anim.SetBool("Idle", false);
                
                if (!_isAttacking) // ÖNEMLÝ KISIM: Sadece saldýrmýyorsak baþlat
                {
                    StartCoroutine(AttackRoutine());
                }
            }
            else
            {
                // Görüþ menzilinde ama saldýrý menzilinde deðilse
                _anim.SetBool("Idle", true);
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackSpeed);

         // Korumayý aç: Artýk yeni saldýrý emri gelemez

        int RandomAttack = Random.Range(1, 13);

        // Önceki saldýrý bool'larýný temizle (Garanti olsun diye)
        ResetAttackBools();

        if (RandomAttack == 1)
        {
            _anim.SetBool("Attack1", true);
            Debug.Log("Attack 1 Baþladý");
            _isAttacking = true;
        }
        else if (RandomAttack == 2)
        {
            _anim.SetBool("Attack2", true);
            Debug.Log("Attack 2 Baþladý");
            _isAttacking = true;
        }
        else if (RandomAttack == 3)
        {
            _anim.SetBool("Attack3", true);
            Debug.Log("Attack 3 Baþladý");
            _isAttacking = true;
        }
        else if (RandomAttack == 11)
        {
            int RandomAttack2 = Random.Range(1, 6);
            if (RandomAttack2 == 2)
            {
                _anim.SetBool("Attack4", true);
                Debug.Log("Attack 4 Baþladý");
                _isAttacking = true;
            }
            if (RandomAttack2 == 3)
            {
                _anim.SetBool("Attack5",true);
            }


        }
       
        // Animasyonun oynadýðýndan emin olmak için bekleme süresi
        yield return new WaitForSeconds(_attackSpeed);

        // Saldýrý bitti, animasyonu kapat
        ResetAttackBools();
        
        // Korumayý kaldýr: Artýk yeni saldýrý yapabilir
        _isAttacking = false; 
    }


    // Kod tekrarýný önlemek için yardýmcý metot
    void ResetAttackBools()
    {
        _anim.SetBool("Attack1", false);
        _anim.SetBool("Attack2", false);
        _anim.SetBool("Attack3", false);
        _anim.SetBool("Attack4", false);
        _anim.SetBool("Attack5", false);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}