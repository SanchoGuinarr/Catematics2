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

        public EquationViewModel()
        {
        }

        [ObservableProperty]
        private string _equationText;

        [RelayCommand]
        public void AddChar(string character)
        {
            _currentNumber += character;
            WeakReferenceMessenger.Default.Send(new NumberInsertedMessage(_currentNumber));
        }

        [RelayCommand]
        public void DeleteChar()
        {
            if (_currentNumber.Length > 0)
            {
                _currentNumber = _currentNumber.Remove(_currentNumber.Length - 1);
            }
            WeakReferenceMessenger.Default.Send(new NumberInsertedMessage(_currentNumber));
        }

        public void SetEquation(IEquation equation)
        {
            EquationText = equation.GetAssignment();
        }
    }
}
