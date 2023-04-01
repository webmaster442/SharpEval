namespace SharpEval.Core
{
    public interface IResultWrtiter
    {
        void Echo(AngleSystem currentAngleSystem, string command);
        void Error(string message);
        void Result(string result);
    }
}
