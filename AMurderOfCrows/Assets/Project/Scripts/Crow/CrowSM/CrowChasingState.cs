using UnityEngine;

namespace MurderOfCrows.Crow.SM
{
    public class CrowChasigState : CrowState
    {
        private Transform _target;

        private Quaternion GetRotation()
        {
            Vector3 direction = _target.position - Controller.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Controller.transform.right, direction);

            return Quaternion.RotateTowards(Controller.transform.rotation, Controller.transform.rotation * rotation, 40 * Time.deltaTime);
        }

        public CrowChasigState(CrowController controller, Transform target) : base(controller)
        {
            _target = target;
        }
        
        public override void Enter() { }

        public override void Exit() { }

        public override void Update()
        {
            if (!_target.gameObject.activeInHierarchy)
                Controller.Burst();

            Controller.transform.rotation = GetRotation();
            Controller.transform.Translate(Vector3.right * Controller.Speed * Time.deltaTime);
        }
    }
}