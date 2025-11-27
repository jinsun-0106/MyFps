using UnityEngine;

namespace MyFps
{
    //로봇 상태 정의
    public enum RobotState
    {
        R_Idle,
        R_Walk,
        R_Attack,
        R_Death
    }

    /// <summary>
    /// 로봇을 관리하는 클래스
    /// 애니메이션, 체력, 이동
    /// </summary>
    public class Robot : MonoBehaviour, IDamageable
    {
        #region Variables
        //참조
        public Animator animator;

        //로봇의 현재 상태
        [SerializeField] private RobotState robotState;
        private RobotState beforeState;

        //체력
        [SerializeField] private float enemyMaxtHP = 20f;
        private float enemyHP;

        private bool isDeath = false;

        //애니메이션 파라미터
        private const string EnemyState = "EnemyState";


        //이동
        [SerializeField] private float moveSpeed = 5f;
        private float currentSpeed;
        private float hurtSpeed = 0f;

        //이동 타겟 - 플레이어
        private Transform thePlayer;

        //대기
        [SerializeField]
        private float idleTimer = 2f;
        private float countdown = 0f;

        //공격
        //공격력 : 5
        [SerializeField] private float attackPower = 5f;
        //공격간격 : 2.0초
        [SerializeField] private float attackTimer = 2.0f;
        //공격범위 : 1.5 안에 있으면 공격
        [SerializeField] private float attackRange = 2f;

        //죽은뒤 리워드 - 탄환
        public GameObject rewardAmmo;

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
            //thePlayer = GameObject.Find("Robot").transform;               //=> 밑이 더 안정적
            thePlayer = FindFirstObjectByType<PlayerMove>().transform;
            currentSpeed = moveSpeed;
        }
        private void Start()
        {
            //초기화
            SetState(RobotState.R_Idle);

            enemyHP = enemyMaxtHP;

            countdown = 0f;

        }
        private void Update()
        {
            if (CannotMove)
            {
                currentSpeed = hurtSpeed;
            }
            else
            {
                currentSpeed = moveSpeed;
            }

            //타겟팅
            Vector3 targetPosition = new Vector3(thePlayer.position.x, thePlayer.position.y, thePlayer.position.z);
            Vector3 dir = targetPosition - transform.position;
            float distance = Vector3.Distance(targetPosition, transform.position);

            //상태 구현
            switch (robotState)
            {
                //3초 후에 겆기로 상태 전환
                case RobotState.R_Idle:
                    countdown += Time.deltaTime;
                    if(countdown >= idleTimer)
                    {
                        //타이머 기능
                        SetState(RobotState.R_Walk);

                        //타이머 초기화
                        countdown = 0f;
                    }
                    break;

                //플레이어를 향해 걷기, 플레이어와의 거리가 2 이내가 되면 공격 상태로 전환
                case RobotState.R_Walk:
                    transform.Translate(dir.normalized * Time.deltaTime * currentSpeed, Space.World);

                    //플레이어와의 거리가 2 이내가 되면 공격 상태로 전환
                    if(distance <= attackRange)
                    {
                        SetState(RobotState.R_Attack);
                    }

                    //타겟을 바라본다
                    transform.LookAt(targetPosition);

                    break;

                //2초마다 데미지 5씩, 플레이어와의 거리가 2가 넘어가면 걷기 상채로 전환 => 타이머 X, 애니메이션에 맞춰서
                case RobotState.R_Attack:
                    /*countdown += Time.deltaTime;
                    if(countdown >= attackTimer)
                    {
                        //타이머 기능
                        EnemyAttack();
                        Debug.Log($"플레이어에게 데미지 {attackPower}를 준다");

                        //타이머 초기화
                        countdown = 0f;
                    }*/

                    //플레이어와의 거리가 2가 넘어가면 되면 걷기 상태로 전환
                    if(distance > attackRange)
                    {
                        SetState(RobotState.R_Walk);
                    }


                    break;

                case RobotState.R_Death:

                    break;
            }

        }

        #endregion

        #region Custom Method
        //로봇의 상태 변경 함수
        private void SetState(RobotState newState)
        {
            if(newState == robotState) return;

            //이전 상채 저장
            beforeState = robotState;

            //새로운 상태로 변경
            robotState = newState;

            //새로운 상태 변경에 따른 구현 내용
            animator.SetInteger(EnemyState, (int)robotState);

            //
            if(robotState == RobotState.R_Death)
            {
                Destroy(gameObject, 6f);
            }

        }

        //데미지 주기
        public void TakeDamage(float damage)
        {
            enemyHP -= damage;
            Debug.Log($"Robot HP: {enemyHP}");

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
            SetState(RobotState.R_Death);

            //리워드
            Instantiate(rewardAmmo,this.transform.position,Quaternion.identity);
        }

        //공격
        public void EnemyAttack()
        {
            //플레이어에게 데미지를 줌(5씩)
            /*PlayerHealth playerHealth = thePlayer.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(attackRange);
            }*/

            IDamageable damageable = thePlayer.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(attackPower);
            }    
        }
        #endregion
    }
}
