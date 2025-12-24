using UnityEngine;
using UnityEngine.AI;

public class MiniEnemyAI : MonoBehaviour
{
    [Header("Hedef Ayarları")]
    public Transform target; // Takip edilecek karakter (Player)

    [Header("Mesafe Ayarları")]
    public float chaseRange = 10f; // Kovalama menzili
    public float attackRange = 2f; // Saldırı menzili

    [Header("Durum")]
    public bool isAttacking = false;

    private NavMeshAgent agent;
    private float distanceToTarget;

    private Animator _anim;
    public HealthSystem healthSystem;
    public HealthSystem healthSystem2;
void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // Eğer hedef atanmamışsa veya prefabdan geliyorsa, sahnedeki Player'ı bul

            if (playerObj != null)
            {
                target = playerObj.transform;
            }
    }

    void Update()
    {

        if (target == null) return;

        // Hedef ile düşman arasındaki mesafeyi ölç
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 1. Durum: Saldırı Menzili içindeyse -> SALDIR
        if (distanceToTarget <= attackRange)
        {
            AttackPlayer();
        }
        // 2. Durum: Kovalama menzili içindeyse ama saldırı menzilinde değilse -> KOVALA
        else if (distanceToTarget <= chaseRange)
        {
            ChasePlayer();
        }
        // 3. Durum: Çok uzaksa -> DUR / DEVRİYE AT
        else
        {
            StopChasing();
        }

        if (healthSystem2._currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ChasePlayer()
    {
        _anim.SetBool("Attack", false);
        _anim.SetBool("Idle", true);

        isAttacking = false;
        agent.isStopped = false;
        agent.SetDestination(target.position); // NavMesh kullanarak hedefe git
    }

    void AttackPlayer()
    {
        _anim.SetBool("Attack", true);
        agent.isStopped = true; // Saldırırken hareket etmeyi durdur
        isAttacking = true;

        // Düşmanın her zaman oyuncuya dönmesini sağla (Yüzünü dönme)
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void StopChasing()
    {
        isAttacking = false;
        agent.isStopped = true; // Dur
    }

    // Editörde menzilleri görmek için yardımcı çizimler
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange); // Kovalama alanı

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Saldırı alanı
    }
}
