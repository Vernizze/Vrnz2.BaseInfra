using System.Collections.Generic;
using Vrnz2.BaseContracts.Interfaces.MessageCodes;
using Vrnz2.BaseContracts.MessageCodes;

namespace Vrnz2.BaseInfra.Translations.Base
{
    public abstract class BaseLocaleMessages
        : ILocaleMessages
    {
        #region Constants

        public const string MessageCode_Base_Ok = "MessageCode_Base_Ok";
        public const string MessageCode_Base_Success = "MessageCode_Base_Success";
        public const string MessageCode_Base_Error = "MessageCode_Base_Error";
        public const string MessageCode_Base_Yes = "MessageCode_Base_Yes";
        public const string MessageCode_Base_No = "MessageCode_Base_No";

        #endregion

        #region Constructors

        protected BaseLocaleMessages()
            => Messages = new List<Message>
                {
                    GetMessageCode_Base_Ok(),
                    GetMessageCode_Base_Success(),
                    GetMessageCode_Base_Error(),
                    GetMessageCode_Base_Yes(),
                    GetMessageCode_Base_No()
                };

        #endregion

        #region Attributes

        public virtual string LocaleName { get; set; } = "en-US";
        public virtual List<Message> Messages { get; set; }

        #endregion

        #region Methods

        protected virtual Message GetMessageCode_Base_Ok()
            => new Message { Code = MessageCode_Base_Ok, Content = "Ok" };

        protected virtual Message GetMessageCode_Base_Success() 
            => new Message { Code = MessageCode_Base_Success, Content = "Success" };

        protected virtual Message GetMessageCode_Base_Error()
            => new Message { Code = MessageCode_Base_Error, Content = "Error" };

        protected virtual Message GetMessageCode_Base_Yes()
            => new Message { Code = MessageCode_Base_Yes, Content = "Yes" };

        protected virtual Message GetMessageCode_Base_No()
            => new Message { Code = MessageCode_Base_No, Content = "No" };

        #endregion
    }
}
