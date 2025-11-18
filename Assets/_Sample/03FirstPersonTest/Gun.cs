using UnityEngine;

namespace MySample
{
    /// <summary>
    /// f키를 누르면 (firepoint에서) 탄환 발사
    /// </summary>
    public class Gun : MonoBehaviour
    {
        #region Variables
        //참조
        public GameObject bulletPrefab;
        public Transform firePoint;

        #endregion

        #region Unity Event Method
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Fire();
            }
        }
        #endregion

        #region Custom Method
        void Fire()
        {
            GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bullet = bulletGo.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.MoveForce();
            }

            //탄환 킬 예약
            Destroy(bulletGo, 3f);
        }
        #endregion
    }
}
