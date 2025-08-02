namespace MurderOfCrows.Crow.SM
{
    public abstract class CrowState
    {
        private CrowController _controller;

        public CrowController Controller => _controller;

        public CrowState(CrowController controller)
        {
            _controller = controller;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}
