using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class ComputingObjectViewModel : ObservableObject
    {
        private IComputingObject _computingObject;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _image;

        [ObservableProperty]
        private bool _canEdit;

        [RelayCommand]
        public void Edit()
        {
            throw new NotImplementedException();
        }

        public ComputingObjectViewModel(IComputingObject computingObject)
        {
            _computingObject = computingObject;
            Title = _computingObject.Title;
            Description = _computingObject.Description;
            Image = _computingObject.ImageSource;
        }

        
    }
}
