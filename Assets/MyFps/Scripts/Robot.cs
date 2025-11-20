using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 로봇을 관리하는 클래스
    /// 애니메이션, 체력, 이동
    /// </summary>
    public class Robot : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;

        //체력
        [SerializeField] private float enemyMaxHP = 20f;

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

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            
        }

        private void Update()
        {
            EnemyMove();
        }

        #endregion

        #region Custom Method
        public void EnemyMove()
        {
            //타겟을 향해 이동하라

            //방향 설정
            Vector3 dir = targatPlayer.position - this.transform.position;

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

                //애니메이션


                //도착 판정 - 타겟과 Enemy 사이의 거리를 구한다
                float distance = Vector3.Distance(this.transform.position, targatPlayer.position);

                //타겟과 Enemy 사이의 거리가 일정 거리 안에 들러오면 도착이라고 판정
                if (distance < attackRange)
                {

                }
            }
        }

        public void EnemyAttack()
        {

        }
        #endregion
    }
}
