using MyFpsUnity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// health를 가진 오브젝트가 죽었을 때 킬 하는 클래스
    /// </summary>
    public class Destructable : MonoBehaviour
    {
        #region Variables
        //참조
        private Health health;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            health = GetComponent<Health>();
        }

        private void Start()
        {
            //health 이벤트 함수 등록
            health.onDeath += OnDie;
            health.onDamaged += OnDamaged;
        }
        #endregion

        #region Custom Method
        //데미지 입었을 때 실행되는 함수
        private void OnDamaged(float damage, GameObject damageSource)
        {
            //데미지 실현 내용
        }

        //죽었을 때 실행되는 함수
        private void OnDie()
        {
            //킬
            Destroy(gameObject);
        }

        #endregion
    }
}
