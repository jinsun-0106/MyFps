using UnityEngine;
using UnityEngine.AI;

namespace MySample
{

    public class AgentController : MonoBehaviour
    {
        #region Variables
        //참조
        private NavMeshAgent m_Agent;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            m_Agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //마우스 클릭한 지점으로 이동
            if(Input.GetMouseButton(0))
            {
                RayToWorld();
            }

        }
        #endregion

        #region
        //마우스 포인터 위치에서 레이를 쏘아 히트한 지점으로 에이전트를 이동시킨다
        private void RayToWorld()
        {
            Vector3 worldPos = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //히드한 지점으로 에이전트를 이동시킨다
                m_Agent.SetDestination(hit.point);
            }

        }
        #endregion

    }
}
    
