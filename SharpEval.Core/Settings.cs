namespace SharpEval.Core
{
    public sealed class Settings
    {
        public AngleSystem CurrentAngleSystem { get; set; }
        public bool EchoExpression { get; set; }

        public Settings()
        {
            CurrentAngleSystem = AngleSystem.Deg;
            EchoExpression = true;
        }
    }
}
