using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FunctionsApp
{
    /// <summary>
    /// Main ViewModel class. Used for two-way data binding
    /// </summary>
    internal class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Array of Functions. Stores information that the user has changed in the form
        /// </summary>
        private FunctionData[] _arrFuncs;

        public MainViewModel()
        {
            _arrFuncs = new FunctionData[Consts.NumberOfFuncs];
            for (int i = 1; i <= Consts.NumberOfFuncs; i++)
            {
                _arrFuncs[i-1] = new FunctionData(i);
            }
        }

        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private int _selectedFunc;
        /// <summary>
        /// The number of function which user selected
        /// </summary>
        public int SelectedFunc
        {
            get 
            { 
                return _selectedFunc;
            }
            set
            {
                _selectedFunc = value;
                OnPropertyChanged(null);
            }
        }


        /// <summary>
        /// The variable which working with coefficient "a". Saves/Loads value into an <see cref="_arrFuncs"/> which user inputted
        /// and makes disables writing non-numeric values
        /// </summary>
        public String ValueA
        {
            get 
            {
                return  _arrFuncs[SelectedFunc].ValueA.ToString(); 
            }
            set 
            {
                if (value.Length > 0)
                {
                    if (value.Length == 1 && value[0] == '-') _arrFuncs[SelectedFunc].ValueA = 0;
                    else if (double.TryParse(value, out double _parseA))
                    {
                        _arrFuncs[SelectedFunc].ValueA = _parseA;
                    }
                }
                else
                {
                    _arrFuncs[SelectedFunc].ValueA = 0;
                }
                OnPropertyChanged("DataGridItems");
            }
        }

        /// <summary>
        /// The variable which working with coefficient "b". Saves/Loads value into an <see cref="_arrFuncs"/> which user inputted
        /// and makes disables writing non-numeric values
        /// </summary>
        public String ValueB
        {
            get 
            { 
                return _arrFuncs[SelectedFunc].ValueB.ToString();
            }
            set 
            {
                if (value.Length > 0)
                {
                    if (double.TryParse(value, out double _parseB))
                    {
                        _arrFuncs[SelectedFunc].ValueB = _parseB;
                    }
                }
                else
                {
                    _arrFuncs[SelectedFunc].ValueB = 0;
                }
                OnPropertyChanged("DataGridItems");
            }
        }

        /// <summary>
        /// The variable which working with coefficient "c". Saves/Loads value into an array of Functions which user selected
        /// </summary>
        public int ValueC
        {
            get 
            { 
                return _arrFuncs[SelectedFunc].ValueC; 
            }
            set
            {
                _arrFuncs[SelectedFunc].ValueC = value;
                OnPropertyChanged("DataGridItems");
            }
        }

        /// <summary>
        /// The variable loads a list of elements with actual values for the selected function
        /// </summary>
        public int[] DataSourceC
        {
            get 
            { 
                return _arrFuncs[SelectedFunc].ArrayC; 
            }
        }

        /// <summary>
        /// The variable loads a collection of <see cref="RowFunction"/>.
        /// </summary>
        public BindingList<RowFunction> DataGridItems
        {
            get 
            {
                _arrFuncs[SelectedFunc].RecalculateFuncsResult();
                return _arrFuncs[SelectedFunc].RowFunctions;
            }
        }

        private RelayCommand _addRowCommand;
        /// <summary>
        /// Command which call method <see cref="AddRow"/> for adding new rows in <see cref="DataGridItems"/>
        /// </summary>
        public ICommand AddRowCommand
        {
            get
            {
                if (_addRowCommand == null)
                {
                    _addRowCommand = new RelayCommand(AddRow);
                }
                return _addRowCommand;
            }
        }

        /// <summary>
        /// Add new row in <see cref="DataGridItems"/>
        /// </summary>
        private void AddRow()
        {
            _arrFuncs[SelectedFunc].RowFunctions.Add(new RowFunction());
            OnPropertyChanged("DataGridItems");
        }


        private RelayCommand _cellEditedCommand;
        /// <summary>
        /// Command which call method <see cref="CellEdited"/> every time when cell editing is end
        /// </summary>
        public ICommand CellEditedCommand
        {
            get
            {
                if (_cellEditedCommand == null)
                {
                    _cellEditedCommand = new RelayCommand(CellEdited);
                }
                return _cellEditedCommand;
            }
        }

        /// <summary>
        /// Call <see cref="PropertyChanged"/> on a <see cref="DataGridItems"/>
        /// </summary>
        private void CellEdited()
        {
            OnPropertyChanged("DataGridItems");
        }
    }
}
