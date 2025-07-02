using UnityEngine;
using UnityEngine.AI;

namespace Remnants
{
    public class FollowPlayer : MonoBehaviour
    {
        #region Variables
        //플레이어를 따라가게 하기 위한 NavMeshAgent
        private NavMeshAgent navMeshAgent;
        private GameObject target;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            navMeshAgent = this.GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            FollowToPlayer();
        }
        #endregion

        #region Custom Method
        //태그가 Player인 오브젝트를 따라가는 코드
        private void FollowToPlayer()
        {
            target = GameObject.FindGameObjectWithTag("Player");

            //target(플레이어)가 null이 아니라면
            if (target != null)
                navMeshAgent.SetDestination(target.transform.position);
        }
        #endregion
    }
}

