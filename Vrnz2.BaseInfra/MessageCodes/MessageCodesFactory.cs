using Vrnz2.BaseContracts.Interfaces.MessageCodes;
using Vrnz2.BaseContracts.MessageCodes;
using Vrnz2.Infra.CrossCutting.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Vrnz2.BaseInfra.MessageCodes
{
    public class MessageCodesFactory
    {
        #region Constants 

        public const string UNEXPECTED_ERROR = "ERROR_00000";

        #endregion

        #region Variables

        private Dictionary<string, List<Message>> _localeMessages = new Dictionary<string, List<Message>>();

        private static MessageCodesFactory _instance;
        private static string MessagesFileName { get; set; } = null;

        #endregion

        #region Constructors

        private MessageCodesFactory()
        {
            var messagesFileName = MessagesFileName ?? "messages.json";

            if (!File.Exists(messagesFileName))
                return;

            var messages = JsonConvert.DeserializeObject<Messages>(File.ReadAllText(messagesFileName, encoding: Encoding.UTF8));

            messages?.LocaleMessages.SForEach(m => _localeMessages.Add(m.LocaleName, m.Messages));
        }

        #endregion

        #region Attributes

        public static MessageCodesFactory Instance
        {
            get
            {
                _instance = _instance ?? new MessageCodesFactory();

                return _instance;
            }
        }

        #endregion

        #region Methods

        public void InitMessages(Assembly assembly)
        {
            var localesMessages =
            (
                from type in assembly.GetTypes()
                where typeof(ILocaleMessages).GetTypeInfo().IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract
                select new { LocaleMessage = (ILocaleMessages)Activator.CreateInstance(type) }
            ).ToList();

            localesMessages.SForEach(m =>
            {
                if (_localeMessages.TryGetValue(m.LocaleMessage.LocaleName, out List<Message> messages))
                {
                    messages.AddRange(m.LocaleMessage.Messages);

                    _localeMessages[m.LocaleMessage.LocaleName] = messages;
                }
                else
                    _localeMessages.Add(m.LocaleMessage.LocaleName, m.LocaleMessage.Messages);
            });
        }

        public string GetMessage(string code)
            => GetMessage(code, Thread.CurrentThread.CurrentCulture.Name);

        public string GetMessage(string code, string cultureName)
        {
            if (_localeMessages.TryGetValue(cultureName, out List<Message> messages))
                return messages.SFirstOrDefault(e => e.Code.Equals(code))?.Content ?? code;

            return code;
        }

        #endregion
    }
}
