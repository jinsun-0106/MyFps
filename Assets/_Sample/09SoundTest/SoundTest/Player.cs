using UnityEngine;

namespace MySample
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody rb;

        [SerializeField] private float forwardForce = 5f;
        //[SerializeField] private float sideForce = 

        private float inputX = 0f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            inputX = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            //앞으로 이동
            rb.AddForce(0f, 0f, forwardForce, ForceMode.Acceleration);

            //좌우 이동
            if(inputX < 0f)
            {
                //rb.AddForce();
            }
            else if(inputX > 0f)
            {

            }
        }
        #endregion
    }
}
