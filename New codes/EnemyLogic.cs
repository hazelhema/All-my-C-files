using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public class EnemyLogic : MonoBehaviour
{
    [SerializeField]
    EnemyState m_currentState = EnemyState.Idle;

    NavMeshAgent m_navMeshAgent;

    [SerializeField]
    Transform m_destination;

    [SerializeField]
    Transform m_patrolStartPosition;

    [SerializeField]
    Transform m_patrolEndPosition;

    Vector3 m_currentPatrolDestination;

    GameObject m_player;

    float m_aggroRadius = 5.0f;
    float m_meleeRadius = 2.0f;
    float m_stoppingDistance = 1.5f;

    const float MAX_ATTACK_COOLDOWN = 0.5f;
    float m_attackCooldown = MAX_ATTACK_COOLDOWN;

    int m_damage = 10;

    int m_health = 100;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip m_enemyAttack;

    [SerializeField]
    AudioClip m_enemyGotHit;

    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_currentPatrolDestination = m_patrolStartPosition.position;

        m_player = GameObject.FindGameObjectWithTag("Player");

        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, m_aggroRadius);

        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, m_meleeRadius);
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_currentState)
        {
            case (EnemyState.Idle):
                SearchForPlayer();
                break;

            case (EnemyState.Patrol):
                SearchForPlayer();
                if (m_patrolEndPosition && m_patrolStartPosition)
                {
                    Patrol();
                }
                break;

            case (EnemyState.Chase):
                ChasePlayer();
                break;

            case (EnemyState.Attack):
                UpdateAttack();
                break;

        }
    }

    void UpdateAttack()
    {
        float distance = Vector3.Distance(transform.position, m_player.transform.position);
        if(distance < m_meleeRadius)
        {
            m_attackCooldown -= Time.deltaTime;

            if (m_attackCooldown < 0.0f)
            {
                // Attack the player
                PlayerLogic playerLogic = m_player.GetComponent<PlayerLogic>();
                if(playerLogic)
                {
                    playerLogic.TakeDamage(m_damage);
                }

                m_attackCooldown = MAX_ATTACK_COOLDOWN;

                PlaySound(m_enemyAttack);
            }
        }
        else
        {
            m_currentState = EnemyState.Chase;
        }
    }

    void ChasePlayer()
    {
        if (m_navMeshAgent && m_destination)
        {
            m_navMeshAgent.SetDestination(m_destination.position);
        }

        float distance = Vector3.Distance(m_destination.position, transform.position);
        if(distance < m_stoppingDistance)
        {
            m_navMeshAgent.isStopped = true;
            m_navMeshAgent.velocity = Vector3.zero;

            m_currentState = EnemyState.Attack;
        }
        else
        {
            m_navMeshAgent.isStopped = false;
        }
    }

    void SearchForPlayer()
    {
        float distance = Vector3.Distance(transform.position, m_player.transform.position);
        if(distance < m_aggroRadius)
        {
            m_currentState = EnemyState.Chase;
        }
    }

    void Patrol()
    {
        if (m_navMeshAgent && m_currentPatrolDestination != Vector3.zero)
        {
            m_navMeshAgent.SetDestination(m_currentPatrolDestination);
        }

        float distance = Vector3.Distance(m_currentPatrolDestination, transform.position);
        if (distance < m_stoppingDistance)
        {
            if(m_currentPatrolDestination == m_patrolStartPosition.position)
            {
                m_currentPatrolDestination = m_patrolEndPosition.position;
            }
            else
            {
                m_currentPatrolDestination = m_patrolStartPosition.position;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        m_health -= damage;

        PlaySound(m_enemyGotHit);

        if (m_health <= 0)
        {
            // Enemy dies
            Destroy(gameObject);
        }
    }

    void PlaySound(AudioClip sound)
    {
        if(m_audioSource && sound)
        {
            m_audioSource.PlayOneShot(sound);
        }
    }
}
