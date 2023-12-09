using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class EquationPageViewModel : ObservableObject, IRecipient<NumberInsertedMessage>, IRecipient<ItemPurchasedMessage>
    {
        private readonly IGeneratorService _generatorService;
        private readonly ISettingsService _settingsService;
        private readonly IComplexityStateService _complexityStateService;
        private readonly ICartService _cartService;
        private readonly IComputingObjectService _computingObjectService;

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

        public EquationPageViewModel(
            IGeneratorService generatorService, 
            ISettingsService settingsService, 
            IComplexityStateService complexityStateService, 
            ICartService cartService,
            IComputingObjectService computingObjectService,
            EquationViewModel equationViewModel
        )
        {
            _generatorService = generatorService;
            _settingsService = settingsService;
            _complexityStateService = complexityStateService;
            _cartService = cartService;
            _computingObjectService = computingObjectService;

            _equationViewModel = equationViewModel;

            _complexityState = _settingsService.Settings.InitialState.GetCopy();

            IsEquationVisible = false;
            IsNewStartVisible = true;

            WeakReferenceMessenger.Default.RegisterAll(this);
            _cartService.GenerateCartItems(_complexityState);
        }

        public void Receive(NumberInsertedMessage message)
        {
            if (_equation.CheckResult(message.Value))
            {
                LogText = $"Správně {_equation.GetWholeEquation()}";
                EquationSolved();
                EquationViewModel.DoCorrectAnimation();
            }
            else if(!string.IsNullOrEmpty(message.Value))
            {
                EquationViewModel.DoIncorrectAnimation();
            }
        }

        private void EquationSolved()
        {
            int newMoney = _equation.Reward;
            _complexityStateService.MoneyCounter.Money += newMoney;
            WeakReferenceMessenger.Default.Send(new MoneyChangedMessage(_complexityStateService.MoneyCounter));
            PrepareNewEquation();
        }

        public void Receive(ItemPurchasedMessage message)
        {
            _complexityStateService.MoneyCounter.Money -= message.Value.Price;
            AddComputingObject(message.Value);  
            _complexityState = message.Value.ModifyState(_complexityState);
            _cartService.CartItems.Remove(message.Value);
            _cartService.GenerateCartItems(_complexityState);
            WeakReferenceMessenger.Default.Send(new MoneyChangedMessage(_complexityStateService.MoneyCounter));
        }

        [RelayCommand]
        public void StartSequence()
        {
            PrepareNewEquation();
            IsEquationVisible = true;
            IsNewStartVisible = false;
        }

        private void AddComputingObject(ICartItem cartItem)
        {
            if (cartItem is IComputingObject computingObject)
            {
                _computingObjectService.ComputingObjects.Add(computingObject);
            }
        }

        private void PrepareNewEquation()
        {
            _equation = _generatorService.GenerateIntEquation(_complexityState);
            EquationViewModel.SetEquation(_equation);
            TryToSolveWithComputingObject();
        }

        private void TryToSolveWithComputingObject()
        {
            IComputingObject foundComputingObject = _computingObjectService.GetEquationSolver(_equation);
            if (foundComputingObject is null)
            {
                return;
            }
            LogText = $"{_equation.GetWholeEquation()} vyřešeno {foundComputingObject.Title}";
            EquationSolved();
        }

    }
}
