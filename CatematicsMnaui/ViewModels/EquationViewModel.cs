using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IEquationSequenceService _equationSequenceService;

        public EquationViewModel(IEquationSequenceService equationSequenceService)
        {
            _equationSequenceService = equationSequenceService;
        }

        [ObservableProperty]
        private string _equation;
    }
}
