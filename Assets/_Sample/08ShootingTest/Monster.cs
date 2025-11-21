using MyFps;
using UnityEngine;

namespace MySample2
{
    /// <summary>
    /// 몬스터들의 부모 클래스
    /// </summary>
    public class Monster : MonoBehaviour
    {
        #region Variables
        //체력
        [SerializeField] private float enemyMaxtHP = 20f;
        private float enemyHP;

        private bool isDeath = false;
        #endregion

        #region Unity Event Method
        protected void Start()
        {
            enemyHP = enemyMaxtHP;
        }

        #endregion

        #region Custom Method
        //데미지 주기
        public void TakeDamage(float damage)
        {
            enemyHP -= damage;
            Debug.Log($"{transform.name} HP: {enemyHP}");

            //죽음체크 - 두번 죽이지 마라
            if (enemyHP <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //죽음 처리
        void Die()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
