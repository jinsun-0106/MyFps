using UnityEngine;
using UnityEngine.Events;

namespace MyFpsUnity.FPS.Game
{
    /// <summary>
    /// 체력을 관리하는 클래스
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float maxHealth = 100f;

        private bool isDeath = false;

        //체력 위험 경계 비율
        [SerializeField]
        private float criticalHealthRatio = 0.3f;

        //이벤트 함수
        public UnityAction<float> onHeal;               //힐 성공시 등록된 함수 호출
        public UnityAction<float, GameObject> onDamaged;            //데미지 입었을 때 등록된 함수 실행
        public UnityAction onDeath;                         //죽었을 때 등록된 함수 실행

        #endregion

        #region Property
        public float CurrentHealth { get; private set; }

        //무적
        public bool Invincible { get; set; }

        //힐 아이템을 먹을 수 있는지 체크
        public bool CamPickup => CurrentHealth < maxHealth;
        //UI : health 게이지량
        public float HealthRatio => CurrentHealth / maxHealth;
        //위험 경고
        public bool IsCritical => HealthRatio <= criticalHealthRatio;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            CurrentHealth = maxHealth;
        }
        #endregion

        #region Custom Method
        public bool Heal(float amount)
        {
            float beforeHealth = CurrentHealth;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

            //real Heal
            float realHeal = CurrentHealth - beforeHealth;

            if(realHeal > 0)
            {
                //힐 효과 구현
                onHeal?.Invoke(amount);

                return true;
            }

            return false;
        }

        //damage: 데미지량, damageSource: 데미지를 주는 주체
        public void TakeDamage(float damage, GameObject damageSource)
        {
            //무적 체크
            if (Invincible)
                return;

            float beforeHealth = CurrentHealth;         //데미지 계산 전의 체력 값
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

            //real Damage 구하기
            float realDamage = beforeHealth - CurrentHealth;

            if(realDamage > 0f)
            {
                //데미지 효과 구현
                onDamaged?.Invoke(damage, damageSource);
            }

            //죽음 처리
            HandleDeath();
        }

        private void HandleDeath()
        {
            //죽음 체크
            if (isDeath == true)
                return;

            if(CurrentHealth <= 0f)
            {
                //죽음 구현
                isDeath = true;

                //죽음 효과 구현
                onDeath?.Invoke();

            }
        }
        #endregion

    }
}
