using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 등록된 문의 열기, 닫기 구현
    /// 인터렉티브 액션으로 이벤트 구현, 인터렉티브 상속 받음
    /// </summary>
    public class DoorSwitch : Interactive
    {
        #region Variables
        public Door door;               //열고 닫을 문 게임 오브젝트

        public Renderer renderer;               //스위치를 그리는 렌더러

        public Material closeMaterial;          //닫을 때 스위치 컬러
        private Material originMaterial;        //열 때 스위티 컬러

        #endregion

        #region Unity Event Method
        protected void Start()
        {
            //초기화
            originMaterial = renderer.material;
        }

        private void OnEnable()
        {
            door.OnActivate += DoorOpen;
            door.OnDeactivate += DoorClose;
        }

        private void OnDisable()
        {
            door.OnActivate -= DoorOpen;
            door.OnDeactivate -= DoorClose;
        }
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(Toggle());
            
        }

        IEnumerator Toggle()
        {

            if(door.IsActive)
            {
                DoorClose();

            }
            else
            {
                DoorOpen();
            }

            yield return new WaitForSeconds(1f);
            //충돌체 다시 나옴
            collider.enabled = true;

        }

        void DoorOpen()
        {
            door.Activate();
            action = "Close The Door";
            renderer.material = closeMaterial;
        }

        void DoorClose()
        {
            door.Deactivate();
            action = "Open The Door";
            renderer.material = originMaterial;
        }
        #endregion
    }
}
