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
    public class Robot : MonoBehaviour
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

        //이동 속도
        [SerializeField] private float moveSpeed = 5f;
        //이동 타겟 - 플레이어
        public Transform targatPlayer;
        //회전 속도
        private float rotateSpeed = 0f;

        //공격력 : 5
        [SerializeField] private float attackPower = 5f;
        //공격간격 : 2.0초
        [SerializeField] private float attackTimer = 2.0f;
        private float countdown = 0f;
        //공격범위 : 1.5 안에 있으면 공격
        [SerializeField] private float attackRange = 1.5f;

        //
        public Door door;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            SetState(RobotState.R_Idle);

            enemyHP = enemyMaxtHP;

        }
        private void Update()
        {
            if (door != null && door.IsActive == true)
            {
                EnemyMove();
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

        }

        //데미지 주기
        public void TakeDamage(float damage)
        {
            enemyHP -= damage;
            Debug.Log($"Robot HP: {enemyHP}");

            //죽음체크 - 두번 죽이지 마라
            if(enemyHP <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //죽음 처리
        void Die()
        {
            //죽는 상태로 변경
            SetState(RobotState.R_Death);


        }

        public void EnemyMove()
        {
            //타겟을 향해 이동하라
            SetState(RobotState.R_Walk);

            //방향 설정
            Vector3 dir = targatPlayer.position - this.transform.position;

            // 도착 판정 - 타겟과 Enemy 사이의 거리를 구한다
            float distance = Vector3.Distance(this.transform.position, targatPlayer.position);

            //타겟과 Enemy 사이의 거리가 일정 거리 안에 들러오면 도착이라고 판정
            if (distance <= attackRange)
            {
                EnemyAttack();
                return;
            }

            //방향 전환 (부드럽게)
            if (dir != Vector3.zero) // 방향 벡터가 0이 아닐 때만 회전 계산
            {
                // 타겟 방향을 바라보는 회전(목표 회전)을 계산
                Quaternion targetRotation = Quaternion.LookRotation(dir);

                // 현재 회전(transform.rotation)에서 목표 회전(targetRotation)으로 
                // 부드럽게(Slerp) 회전합니다.
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

                //타겟 방향으로 이동
                this.transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);

            }
        }

        public void EnemyAttack()
        {
            SetState(RobotState.R_Attack);
        }
        #endregion
    }
}
