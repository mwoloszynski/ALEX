using ALEX.Helpers;
using DBMODEL.Entities;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ALEX.ViewModels.Windows
{
    public class StockViewModel : NotificationObject, IDataErrorInfo
    {
        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand;

        private string _name;
        private string _code;
        private Exchange _exchange;
        private string _error;

        private List<Exchange> _exchangeList;

        private string OnValidate(string columnName)
        {
            string result = string.Empty;
            switch (columnName)
            {
                case "Name":
                    if(string.IsNullOrEmpty(Name))
                    {
                        result = "Name is required";
                    }
                    break;
                case "Code":
                    if(string.IsNullOrEmpty(Code))
                    {
                        result = "Code is required";
                    }
                    break;
                case "Exchange":
                    if(Exchange == null)
                    {
                        result = "Exchange is required";
                    }
                    else if(Exchange.Id == 0)
                    {
                        result = "Exchange is required";
                    }
                    break;
                default:
                    result = null;
                    break;
            }
            if (result == null)
            {
                Error = "Error";
            }
            return result;
        }

        private void Submit()
        {

        }

        public StockViewModel()
        {
            _exchangeList = new List<Exchange>();
            _exchangeList.Add(new Exchange() { Code = "PL", Name = "Polska" });
            _exchangeList.Add(new Exchange() { Code = "US", Name = "USA" });
            _submitCommand = new RelayCommand(Submit);
        }

        public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }



        public List<Exchange> ExchangeList => _exchangeList;
        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;
                    RaisePropertyChanged(nameof(Error));
                }
            }
        }
        public Exchange Exchange
        {
            get => _exchange;
            set
            {
                _exchange = value;
                RaisePropertyChanged(nameof(Exchange));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }
    }
}
