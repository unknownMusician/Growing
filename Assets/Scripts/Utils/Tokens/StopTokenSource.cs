namespace Growing.Utils.Tokens
{
    public class StopTokenSource
    {
        public bool IsStopped { get; private set; }

        public StopToken Token => new(this);

        public void Stop()
        {
            IsStopped = true;
        }
    }
}