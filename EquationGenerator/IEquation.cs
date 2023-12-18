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
        /// <summary>
        /// Returns true if result is not complete (i.e. is empty string or only one digit for two digit result)
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        bool ResultIsNotComplete(string result);
        string GetAssignment();
        string GetWholeEquation();
    }
}