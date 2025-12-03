using UnityEngine;
using UnityEngine.Events;

namespace MyFps
{
    /// <summary>
    /// 문(door) 열기/닫기
    /// </summary>
    public class Door : MonoBehaviour, ISwitchable
    {
        #region Variables
        //참조
        protected Animator animator;

        //true면 문이 열려 있는 상태, false면 닫혀있는 상태
        [SerializeField]
        protected bool isActive;

        public UnityAction OnActivate;
        public UnityAction OnDeactivate;

        //적 등록
        public GunMan[] enemies;

        //사운드

        //애니메이터 파라미터
        const string IsOpen = "IsOpen";
        #endregion

        #region Property
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                animator.SetBool(IsOpen, value);

                //사운드 플레이
            }
        }
        #endregion

        #region Unity Event Method
        protected virtual void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }

        /*protected void OnEnable()
        {
            if(enemy)
            {
                //문 열때
                OnActivate += enemy.OnActive;
                //문 닫을 때
                OnDeactivate += enemy.GoBack;
            }
        }

        protected void OnDisable()
        {
            if (enemy)
            {
                OnActivate -= enemy.OnActive;
                OnDeactivate -= enemy.GoBack;
            }
        }
*/
        protected virtual void Start()
        {
            //문 상태 열림/닫힘 설정
            if (isActive)
            {
                Activate();
            }
        }
        #endregion

        #region Custom Method
        public void Activate()
        {
            IsActive = true;

            //활성화시 등록된 함수 호출
            OnActivate?.Invoke();

            foreach (var enemy in enemies)
            {
                enemy.OnActive();
            }
        }

        public void Deactivate()
        {
            IsActive = false;

            //비 활성화시 등록된 함수 호출
            OnDeactivate?.Invoke();

            foreach (var enemy in enemies)
            {
                enemy.GoBack();
            }
        }
        #endregion
    }
}