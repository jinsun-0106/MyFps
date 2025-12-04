using System.Globalization;
using UnityEngine;
using UnityEngine.AI;

namespace MyFps
{
    //적 공통 상태 정의
    public enum EnemyState
    {
        E_Idle,             //대기
        E_Walk,             //패트롤
        E_Chase,            //추격
        E_Attack,           //공격
        E_Death             //죽음
    }

    /// <summary>
    /// GunMan Enemy를 관리하는 클래스
    /// 애니메이션, 체력, 이동
    /// </summary>
    public class GunMan : MonoBehaviour, IDamageable
    {
        #region Variables
        //참조
        private Animator animator;
        private NavMeshAgent agent;
        private Transform thePlayer;

        //건맨의 상태 관리
        [SerializeField] private EnemyState currentState;           //현재 상태
        private EnemyState beforeState;                             //이전 상태

        //체력
        [SerializeField] private float enemyMaxtHP = 20f;
        private float enemyHP;
        private bool isDeath = false;
        [SerializeField] private float destroyDlay = 6f;

        //애니메이터 파라미터
        private const string MoveSpeed = "MoveSpeed";
        const string IsDeath = "IsDeath";
        const string Fire = "Fire";

        //상태 - 대기
        [SerializeField]
        private float idleTimer = 2f;
        private float countdown = 0f;

        //맞을 때
        private float currentSpeed;
        private float hurtSpeed = 0f;

        //상태 - 패트롤
        public Transform[] wayPoints;
        private int wayPointIndex = 0;
        [SerializeField]
        private bool isPatroll = false;
        //처음 생성 위치
        private Vector3 startPosition = Vector3.zero;

        //상태 - 추격
        [SerializeField]
        private float detectDistance = 10f;                 //적이 거리 안에 들어오면 추격 시작

        //상태 - 공격
        [SerializeField]
        private float attackRange = 5f;                     //적이 사거리 안에 들어오면 추격을 멈추고 공격 시작
        [SerializeField]
        private float attackTimer = 2f;                     //2초에 한 번씩 발사
        [SerializeField]
        private float attackDamage = 5f;                    //발사시 플레이어에게 attackDamage 준다

        private bool isBack = false;
        #endregion

        #region Property
        //애니메이터의 파라미터값(CannotMove) 읽어오기
        public bool CannotMove
        {
            get
            {
                return animator.GetBool("CannotMove");
            }
        }

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            thePlayer = FindFirstObjectByType<PlayerMove>().transform;
        }

        private void Start()
        {
            //초기화
            enemyHP = enemyMaxtHP;
            wayPointIndex = 0;
            startPosition = transform.position;

            currentSpeed = agent.velocity.magnitude;

            SetState(EnemyState.E_Idle);
        }

        private void Update()
        {
            //죽음 체크
            if (isDeath) return;

            if (CannotMove)
            {
                currentSpeed = hurtSpeed;
            }
            else
            {
                currentSpeed = agent.velocity.magnitude;
            }

            //디텍팅
            float distance = Vector3.Distance(thePlayer.position, transform.position);
            if (distance <= attackRange && isBack == false)                //공격 거리 체크
            {
                SetState(EnemyState.E_Attack);
            }
            else if(distance <= detectDistance && isBack == false)         //디텍팅 거리 체크
            {
                SetState(EnemyState.E_Chase);
            }

            //상태 처리
            switch(currentState)
            {
                case EnemyState.E_Idle:
                    if(isPatroll)
                    {
                        countdown += Time.deltaTime;
                        if (countdown >= idleTimer)
                        {
                            //타이머 기능
                            SetState(EnemyState.E_Walk);
                        }
                    }
                    break;

                case EnemyState.E_Walk:         //패트롤
                    //이동 : agent로
                    //wayPoints[0].position 이동 -> 도착 후 대기
                    //-> wayPoints[1].position 이동 -> 도착 후 대기
                    //-> wayPoints[2].position 이동 -> 도착 후 대기 -> wayPoints[0].position 이동 -> 도착 후 대기 ...
                    //agent.remainingDistance : 도착지점까지 남은 거리

                    //agent.SetDestination(wayPoints[wayPointIndex].position);

                    //도착 판정
                    if (agent.remainingDistance < 0.1f)
                    {
                        if(isPatroll)
                        {
                            wayPointIndex++;
                            if (wayPointIndex >= wayPoints.Length)
                            {
                                wayPointIndex = 0;
                            }
                        }
                        SetState(EnemyState.E_Idle);
                    }
                    break;

                case EnemyState.E_Chase:
                    agent.SetDestination(thePlayer.position);

                    if (distance > detectDistance)         //추격 실패시 다시 패트롤로
                    {
                        SetState(EnemyState.E_Walk);
                    }
                        break;

                case EnemyState.E_Attack:
                    countdown += Time.deltaTime;
                    if (countdown >= attackTimer)
                    {
                        //타이머 기능
                        EnemyShoot();

                        //타이머 초기화
                        countdown = 0f;
                    }
                    break;

            }

            //애니메이터 파라미터 처리
            animator.SetFloat(MoveSpeed, agent.velocity.magnitude);
        }

        //디텍팅 거리 기즈모 그리기
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, detectDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);

        }

        #endregion

        #region Custom Method
        //건맨의 상태 변경 함수
        private void SetState(EnemyState newState)
        {
            if (newState == currentState) return;

            //이전 상채 저장
            beforeState = currentState;

            //새로운 상태로 변경
            currentState = newState;

            //agent 초기화
            agent.ResetPath();

            //상태별 초기값 설정
            switch (currentState)
            {
                case EnemyState.E_Idle:
                    //타이머 초기화
                    idleTimer = Random.Range(2f, 3f);
                    countdown = 0f;
                    break;

                case EnemyState.E_Walk:
                    //이동 목표 지점
                    if (isPatroll)
                    {
                        agent.SetDestination(wayPoints[wayPointIndex].position);
                    }
                    else
                    {
                        agent.SetDestination(startPosition);
                    }
                    break;

                case EnemyState.E_Attack:
                    //멈춤 - 이동 목표 지점을 현재 위치로 지정
                    agent.SetDestination(this.transform.position);
                    break;

                case EnemyState.E_Death:
                    //애니메이션
                    animator.SetBool(IsDeath, true);
                    break;

            }

            if(currentState == EnemyState.E_Chase || currentState == EnemyState.E_Attack)
            {
                animator.SetLayerWeight(1, 1f);
            }
            else
            {
                animator.SetLayerWeight(1, 0f);
            }

            //타이머 초기화
            countdown = 0f;

        }

        public void TakeDamage(float damage)
        {
            enemyHP -= damage;
            
            //효과(vfx, sfx), UI처리, 애니메이션 등
            animator.SetTrigger("IsHurt");

            //죽음체크 - 두번 죽이지 마라
            if (enemyHP <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //죽음 처리
        void Die()
        {
            isDeath = true;

            //죽는 상태로 변경
            SetState(EnemyState.E_Death);

            //효과(vfx, sfx), UI처리, 애니메이션, 보상 등            
            /*if (rewardAmmo != null)
            {
                //리워드
                Instantiate(rewardAmmo, this.transform.position, Quaternion.identity);
            }*/

            //킬
            Destroy(gameObject, destroyDlay);
            
        }

        //총 발사
        public void EnemyShoot()
        {
            //애니메이션
            animator.SetTrigger(Fire);

            //효과(vfx, sfx), UI처리, 애니메이션, 보상 등

            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }

        }

        //문 열 때 호출되는 함수
        public void OnActive()
        {
            isBack = false;
        }

        //문이 닫힐때 호출되는 함수
        public void GoBack()
        {
            isBack = true;
            SetState(EnemyState.E_Walk);
        }

        #endregion
    }
}
