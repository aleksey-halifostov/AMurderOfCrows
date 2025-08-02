using UnityEngine;
using UnityEngine.Splines;

namespace MurderOfCrows.Crow.SM
{
    public class CrowMoveState : CrowState
    {
        private Spline _spline;
        private float _movementCoeficient;
        private float _progress = 0f;
        private SplineFollower _follower;

        private void MoveController()
        {
            var current = _follower.Evaluate(_progress);
            Controller.transform.position = current.position;
            Controller.transform.rotation = current.rotation;
        }

        public CrowMoveState(CrowController controller, Spline spline) : base(controller)
        {
            _spline = spline;
            _follower = new SplineFollower(Controller.transform, spline);
            _movementCoeficient = 1 / _spline.GetLength();
        }

        public override void Enter()
        {

        }

        public override void Exit() 
        {
            
        }

        public override void Update()
        {
            _progress += Controller.Speed * _movementCoeficient * Time.deltaTime;

            if (_progress >= 1)
            {
                Controller.Burst();
            }

            MoveController();
        }
    }
}
