using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFps
{
    /// <summary>
    /// 플레이어의 Health 관리 클래스
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables

        //체력
        private float playerHP;
        [SerializeField] private float playerMaxHP = 50f;
        private bool isDeath = false;

        //데미지 입을 때 등록된 함수 호출
        public UnityAction onDamage;

        //죽었을 때 호출되는 함수 호출
        public UnityAction onDie;

        //데미지 UI
        public Image playerHPImage;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            playerHP = playerMaxHP;
        }

        #endregion

        #region Custom Method
        //데미지 주기
        public void TakeDamage(float damage)
        {
            playerHP -= damage;
            //Debug.Log($"Player HP: {playerHP}");

            //데미지 이펙트
            onDamage?.Invoke();

            //데미지 UI
            playerHPImage.fillAmount = playerHP / playerMaxHP;

            //죽음체크 - 두번 죽이지 마라
            if (playerHP <= 0f && isDeath == false)
            {
                Die();
            }
        }
        private void Die()
        {
            //Debug.Log("Go to GameOver");
            onDie?.Invoke();
        }

        #endregion
    }
}
