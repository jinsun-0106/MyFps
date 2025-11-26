using UnityEngine;
using TMPro;

namespace MyFps
{
    public class DrawAmmoCount : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI ammoCountText;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            ammoCountText.text = PlayerStats.Instance.AmmoCount.ToString();

        }
        #endregion

    }
}
