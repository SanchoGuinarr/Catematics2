using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class EquationPageViewModel : ObservableObject, IRecipient<NumberInsertedMessage>
    {
        private readonly IGeneratorService _generatorService;
        private readonly ISettingsService _settingsService;

        private bool _isSequence = false;
        private ComplexityState _complexityState;
        private IEquation _equation;

        [ObservableProperty]
        private EquationViewModel _equationViewModel;

        [ObservableProperty]
        private bool _isEquationVisible;

        [ObservableProperty]
        private bool _isNewStartVisible;

        [ObservableProperty]
        private string _logText;

        public EquationPageViewModel(IGeneratorService generatorService, ISettingsService settingsService, EquationViewModel equationViewModel)
        {
            _generatorService = generatorService;
            _settingsService = settingsService;
            _equationViewModel = equationViewModel;

            _complexityState = _settingsService.Settings.InitialState;

            IsEquationVisible = false;
            IsNewStartVisible = true;

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(NumberInsertedMessage message)
        {
            if (_equation.CheckResult(message.Value))
            {
                PrepareNewEquation();
                LogText = "Správně";
            }
            else
            {
                LogText = "Špatně";
            }
        }

        [RelayCommand]
        public void StartSequence()
        {
            PrepareNewEquation();
            IsEquationVisible = true;
            IsNewStartVisible = false;
        }

        private void PrepareNewEquation()
        {
            _equation = _generatorService.GenerateIntEquation(_complexityState);
            EquationViewModel.SetEquation(_equation);
        }

    }
}
