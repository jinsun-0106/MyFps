using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 카메라 흔들림 연출 구현, 싱글톤 클래스 상속
    /// 흔들림함수(흔들림 시간, 흔들림 크기, 흔들림 속도)
    /// </summary>
    public class CinemachineShake : Singleton<CinemachineShake>
    {
        #region Variables
        private CinemachineShake cinemachineShake;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            cinemachineShake = GetComponent<CinemachineShake>();
        }

        private void Update()
        {
            
        }
        #endregion

        #region Custom Method
        IEnumerator CameraShake()
        {
            //흔들림 효과

            yield return new WaitForSeconds(1f);
        }
        #endregion
    }
}
