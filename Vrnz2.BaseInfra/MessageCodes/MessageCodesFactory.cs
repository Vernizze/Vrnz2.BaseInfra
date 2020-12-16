using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Vrnz2.BaseContracts.MessageCodes;

namespace Vrnz2.BaseInfra.MessageCodes
{
    public class MessageCodesFactory
    {
        #region Constants 

        public const string UNEXPECTED_ERROR = "ERROR_00000";

        #endregion

        #region Variables

        private static MessageCodesFactory _instance;
        private static string MessagesFileName { get; set; } = null;

        #endregion

        #region Constructors

        private MessageCodesFactory() 
            => Init();

        #endregion

        #region Attributes

        public static MessageCodesFactory Instance
            => SetInstance();

        public string LocaleName { get; private set; }

        public List<Message> ErrorMessages { get; private set; }

        #endregion

        #region Methods

        private static MessageCodesFactory SetInstance() 
        {
            _instance = _instance ?? new MessageCodesFactory();

            return _instance;
        }

        private void Init()
        {
            var messagesFileName = MessagesFileName ?? "messages.json";

            if (!File.Exists(messagesFileName))
                return;

            var culture = Thread.CurrentThread.CurrentCulture;

            var errorMessages = JsonConvert.DeserializeObject<Messages>(File.ReadAllText(messagesFileName));

            var currentErrorMessage = errorMessages.LocaleMessages.First(e => e.LocaleName.Equals(culture.Name));

            LocaleName = currentErrorMessage.LocaleName;
            ErrorMessages = currentErrorMessage.Messages;
        }

        public string GetMessage(string errorCode)
        {
            if (ErrorMessages.Any(e => e.Code.Equals(errorCode)))
                return ErrorMessages.FirstOrDefault(e => e.Code.Equals(errorCode)).Content;
            else
                return string.Empty;
        }

        #endregion
    }
}
