namespace EquationGenerator
{
    public interface IEquation
    {
        int Complexity { get; set; }
        ANumber FirstNumber { get; set; }
        ANumber Result { get; set; }
        int Reward { get; }
        ANumber SecondNumber { get; set; }
        NumberType Type { get; set; }

        bool CheckResult(int result);
        bool CheckResult(string result);
        string GetAssignment();
        string GetWholeEquation();
    }
}