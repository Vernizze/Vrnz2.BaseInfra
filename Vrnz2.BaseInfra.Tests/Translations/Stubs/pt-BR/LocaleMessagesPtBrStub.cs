using Vrnz2.BaseContracts.MessageCodes;
using Vrnz2.BaseInfra.Translations.Base;

namespace Vrnz2.BaseInfra.Tests.Translations.Stubs.pt_BR
{
    public class LocaleMessagesPtBrStub
        : BaseLocaleMessages
    {
        #region Attributes

        public override string LocaleName { get; set; } = "pt-BR";

        #endregion

        #region Methods

        protected override Message GetMessageCode_Base_Ok()
            => new Message { Code = MessageCode_Base_Ok, Content = "Ok" };

        protected override Message GetMessageCode_Base_Success()
            => new Message { Code = MessageCode_Base_Success, Content = "Sucesso" };

        protected override Message GetMessageCode_Base_Error()
            => new Message { Code = MessageCode_Base_Error, Content = "Erro" };

        protected override Message GetMessageCode_Base_Yes()
            => new Message { Code = MessageCode_Base_Yes, Content = "Sim" };

        protected override Message GetMessageCode_Base_No()
            => new Message { Code = MessageCode_Base_No, Content = "Não" };

        #endregion
    }
}
