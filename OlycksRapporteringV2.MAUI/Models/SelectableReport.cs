
using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ZstdSharp.Unsafe;

namespace OlycksRapporteringV2.MAUI.Models
{
    public class SelectableReport : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Report Report { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CheckBoxIcon));
                OnPropertyChanged(nameof(CardColor));
            }
        }
        public Color CardColor => IsSelected
            ? Color.FromArgb("#E83B3B")
            : Color.FromArgb("#1A1D27");
        public string CheckBoxIcon => IsSelected ? "●" : "○";

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public string ShortDescription => Report.ReportDescription?.Length > 60
            ? Report.ReportDescription.Substring(0, 60) + "..."
            : Report.ReportDescription;

        public string CreatedByName { get; set; }

        private bool _isSelectedForEdit;
        public bool IsSelectedForEdit 
        {
            get => _isSelectedForEdit;
            set
            {
                _isSelectedForEdit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EditSelectedColor));
            } 
        }
        public Color EditSelectedColor => IsSelectedForEdit
            ? Color.FromArgb("#1A2A1A")
            : Color.FromArgb("1A1D27");


    }
}
