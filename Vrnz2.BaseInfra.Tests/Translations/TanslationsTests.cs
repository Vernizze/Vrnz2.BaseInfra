using FluentAssertions;
using me_web_automation_transaction_abstractions.Translations.Stubs.en_US;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.MessageCodes;
using Vrnz2.BaseInfra.Translations.Base;
using Vrnz2.TestUtils;
using Xunit;

namespace Vrnz2.BaseInfra.Tests.Translations
{
    public class TanslationsTests
        : AbstractTest
    {
        public const string MessageCode_Base_Ok = "MessageCode_Base_Ok";
        public const string MessageCode_Base_Success = "MessageCode_Base_Success";
        public const string MessageCode_Base_Error = "MessageCode_Base_Error";
        public const string MessageCode_Base_Yes = "MessageCode_Base_Yes";
        public const string MessageCode_Base_No = "MessageCode_Base_No";

        [Theory]
        [InlineData("pt-BR", BaseLocaleMessages.MessageCode_Base_Ok, "Ok")]
        [InlineData("en-US", BaseLocaleMessages.MessageCode_Base_Ok, "Ok")]
        [InlineData("pt-BR", BaseLocaleMessages.MessageCode_Base_Success, "Sucesso")]
        [InlineData("en-US", BaseLocaleMessages.MessageCode_Base_Success, "Success")]
        [InlineData("pt-BR", BaseLocaleMessages.MessageCode_Base_Error, "Erro")]
        [InlineData("en-US", BaseLocaleMessages.MessageCode_Base_Error, "Error")]
        [InlineData("pt-BR", BaseLocaleMessages.MessageCode_Base_Yes, "Sim")]
        [InlineData("en-US", BaseLocaleMessages.MessageCode_Base_Yes, "Yes")]
        [InlineData("pt-BR", BaseLocaleMessages.MessageCode_Base_No, "Não")]
        [InlineData("en-US", BaseLocaleMessages.MessageCode_Base_No, "No")]
        public void MessageCodesFactory_GetMessage_WithoutLocale_When_PassingMessageCode_Should_ExpectedMessageContent(string locale, string messageCode, string localeMessage)
        {
            // Arrange
            IServiceCollection services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);

            services.AddTranslations(AssembliesHelper.GetAssemblies<LocaleMessagesEnUsStub>());

            // Act
            var message = MessageCodesFactory.Instance.GetMessage(messageCode);

            // Assert
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be(localeMessage);
        }
    }
}
