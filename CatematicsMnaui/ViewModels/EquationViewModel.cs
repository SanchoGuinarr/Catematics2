using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class EquationViewModel : ObservableObject
    {
        private IEquation _equation;
        private string _currentNumber;
        private string _assignmentText;

        public EquationViewModel()
        {
        }

        [ObservableProperty]
        private string _equationText;

        [RelayCommand]
        public void Digit(string character)
        {
            _currentNumber += character;
            EquationText = _assignmentText + _currentNumber;
            WeakReferenceMessenger.Default.Send(new NumberInsertedMessage(_currentNumber));
        }

        [RelayCommand]
        public void Clear(string character)
        {
            if (_currentNumber.Length == 0)
            {
                return;
            }
            _currentNumber = "";
            EquationText = _assignmentText + _currentNumber;
            WeakReferenceMessenger.Default.Send(new NumberInsertedMessage(_currentNumber));
        }

        [RelayCommand]
        public void Backspace()
        {
            if (_currentNumber.Length == 0)
            {
                return;
            }
            _currentNumber = _currentNumber.Remove(_currentNumber.Length - 1);
            EquationText = _assignmentText + _currentNumber;
            WeakReferenceMessenger.Default.Send(new NumberInsertedMessage(_currentNumber));
        }

        public void SetEquation(IEquation equation)
        {
            _assignmentText = equation.GetAssignment();
            EquationText = _assignmentText;
        }
    }
}
