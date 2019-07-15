using System;
using System.Collections.Generic;
using System.Text;

namespace BookSys.ViewModel.ViewModel
{
    public class ResponseVM
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Identifier { get; set; }
        public Exception ExceptionError { get; set; }

        public ResponseVM(string action, bool isSuccess, string entity, string msg = "", string identifier = "", Exception exception = null)
        {
            IsSuccess = isSuccess;
            if(isSuccess)
            {
                Message = $"{entity} was successfully {action}. {msg}";
                Identifier = identifier;
            } else
            {
                Message = $"{entity} was unccessfully {action}. {msg}";
                ExceptionError = exception;
                if (identifier != null)
                    Identifier = identifier;
            }
        }

        public static string SOMETHING_WENT_WRONG { get { return "Something went wrong! Please try again."; } }
        public static string DOES_NOT_EXIST { get { return "It does not exist! It might not have been deleted or editd by other user, thry refreshing the page"; } }
        public static string ALREADY_EXIST { get { return "It already Exist!"; } }
        public static string NO_NEW_DATA { get { return "You did not update anything! Sent data is the same as the previous data!"; } }
    }
}
