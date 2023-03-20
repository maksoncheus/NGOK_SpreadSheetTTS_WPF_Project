using Newtonsoft.Json.Linq;
using NGOK_SpreadSheetTTS_WPF_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

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
        public string WeekDay
        {
            get => _info.WeekDay;
        }
        private ObservableCollection<string> _groups;
        public ObservableCollection<string> Groups
        {
            get => _groups; 
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
                SelectedGroup = 0;
            }
        }
        private int _selectedGroup;
        public int SelectedGroup
        { 
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                _info = Models.SpreadSheetModel.GetSheet(value);
                OnPropertyChanged(nameof(SelectedGroup));
                OnPropertyChanged(nameof(SpreadSheetGrid));
                if(value >= 0) Speech.Execute(null);
            }
        }
        public ICommand SwitchSheet { get; }
        public ICommand Speech { get; }

        public ApplicationViewModel()
        {
            SwitchSheet = new CommonCommand<string>
            (
                obj => {
                    Initializaton.sheet = obj;
                    Groups = Models.SpreadSheetModel.GetGroups();
                }
                , (obj) => true
            );
            Speech = new CommonCommand
            (
                            () => { SpeechModel.Speech(Groups[_selectedGroup], SpreadSheetGrid); },
                            () => true
            );
            Groups = Models.SpreadSheetModel.GetGroups();
        }
    }
}
