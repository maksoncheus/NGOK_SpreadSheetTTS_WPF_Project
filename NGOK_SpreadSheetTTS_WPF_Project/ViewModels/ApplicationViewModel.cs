using NGOK_SpreadSheetTTS_WPF_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NGOK_SpreadSheetTTS_WPF_Project.ViewModels
{
    internal class ApplicationViewModel : ApplicationViewModelBase
    {
        private (string WeekDay, DataTable Spreadsheet) _info;
        public DataTable SpreadSheetGrid
        {
            get => _info.Spreadsheet;
            set
            {
                OnPropertyChanged(nameof(SpreadSheetGrid));
            }
        }
        public ObservableCollection<string> Groups { get; set; }
        private int _selectedGroup;
        public int SelectedGroup { get => _selectedGroup; set { _selectedGroup = value; _info = Models.SpreadSheetModel.GetSheet(value); SpreadSheetGrid = _info.Spreadsheet; OnPropertyChanged(nameof(SelectedGroup)); } }
        public ApplicationViewModel()
        {
            Groups = Models.SpreadSheetModel.GetGroups();
            _info = Models.SpreadSheetModel.GetSheet(0);
            SpreadSheetGrid = _info.Spreadsheet;
        }
    }
}
