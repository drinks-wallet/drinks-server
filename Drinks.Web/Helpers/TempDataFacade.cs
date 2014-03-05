using System.Web.Mvc;

namespace Drinks.Web.Helpers
{
    public class TempDataFacade
    {
        const string WarningMessageKey = "WarningMessage";
        const string SuccessMessageKey = "SuccessMessage";
        const string DangerMessageKey = "DangerMessage";
        const string InfoMessageKey = "InfoMessage";

        readonly TempDataDictionary _tempDataDictionary;

        public TempDataFacade(TempDataDictionary tempDataDictionary)
        {
            _tempDataDictionary = tempDataDictionary;
        }

        public string DangerMessage
        {
            get { return Get(DangerMessageKey); }
            set { Set(DangerMessageKey, value); }
        }

        public string InfoMessage
        {
            get { return Get(InfoMessage); }
            set { Set(InfoMessage, value); }
        }

        public string SuccessMessage
        {
            get { return Get(SuccessMessageKey); }
            set { Set(SuccessMessageKey, value); }
        }

        public string WarningMessage
        {
            get { return Get(WarningMessageKey); }
            set { Set(WarningMessageKey, value); }
        }

        string Get(string key)
        {
            return _tempDataDictionary[key] as string;
        }

        void Set(string key, string value)
        {
            _tempDataDictionary[key] = value;
        }
    }
}