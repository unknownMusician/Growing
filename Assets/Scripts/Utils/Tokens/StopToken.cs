namespace Growing.Utils.Tokens
{
    public struct StopToken
    {
        private readonly StopTokenSource stopTokenSource;

        public bool IsStopped => stopTokenSource.IsStopped;
        
        public StopToken(StopTokenSource stopTokenSource)
        {
            this.stopTokenSource = stopTokenSource;
        }
    }
}