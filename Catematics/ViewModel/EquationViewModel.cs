using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catematics.ViewModel
{
    class EquationViewModel : ObservableObject
    {
        private AEquation equation;

        public event EventHandler<AEquation> SuccessfullyComputed;

        public ICommand ConfirmCommand { get; set; }
        public string Assignment { get; private set; }
        public string Result { get; set; }

        public string Reward { get; private set; }

        public EquationViewModel(AEquation Equation)
        {
            equation = Equation;
            Assignment = equation.GetAssignment();
            Reward = equation.Reward.ToString();
            // ButtonTestCommand = new RelayCommand<object>(new Action<object>(ButtonScrewClicked), new Predicate<object>(CanScrew));
            ConfirmCommand = new RelayCommand<object>(new Action<object>(ResultConfirmed));
        }

        private void ResultConfirmed(object obj)
        {
            if (equation.CheckResult(Result))
            {
                SuccessfullyComputed?.Invoke(this, equation);
            }
            else
            {
                // nějaká animace
            }
            Result = "";
            OnPropertyChange(nameof(Result));
        }
    }
}
