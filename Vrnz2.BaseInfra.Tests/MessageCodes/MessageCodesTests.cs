using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Vrnz2.BaseContracts.Interfaces.MessageCodes;
using Vrnz2.BaseContracts.MessageCodes;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.MessageCodes;
using Vrnz2.TestUtils;
using Xunit;

namespace Vrnz2.BaseInfra.Tests.MessageCodes
{
    public class MessageCodesTests
        : AbstractTest
    {
        [Theory]
        [InlineData("pt-BR", "Message01", "Não encontrado!")]
        [InlineData("pt-BR", "Message02", "Sucesso!")]
        [InlineData("pt-BR", "Message03", "Cadastro já existente!")]
        [InlineData("en-US", "Message01", "Not Found!")]
        [InlineData("en-US", "Message02", "Success!")]
        [InlineData("en-US", "Message03", "Register already exists!")]
        public void MessageCodesFactory_GetMessage_WithoutLocale_When_PassingMessageCode_Should_ExpectedMessageContent(string locale, string messageCode, string localeOneMessage)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);

            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var message = MessageCodesFactory.Instance.GetMessage(messageCode);

            // Assert
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be(localeOneMessage);
        }

        [Theory]
        [InlineData("pt-BR", "Message01", "Não encontrado!")]
        [InlineData("en-US", "Message01", "Not Found!")]
        [InlineData("pt-BR", "Message02", "Sucesso!")]
        [InlineData("en-US", "Message02", "Success!")]
        [InlineData("pt-BR", "Message03", "Cadastro já existente!")]
        [InlineData("en-US", "Message03", "Register already exists!")]
        public void MessageCodesFactory_GetMessage_WithLocale_When_PassingMessageCode_Should_ExpectedMessageContent(string locale, string messageCode, string message)
        {
            // Arrange
            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var messageFound = MessageCodesFactory.Instance.GetMessage(messageCode, locale);

            // Assert
            messageFound.Should().NotBeNullOrWhiteSpace();
            messageFound.Should().Be(message);
        }

        [Theory]
        [InlineData("pt-BR")]
        [InlineData("en-US")]
        public void MessageCodesFactory_GetMessage_WithoutLocalePAramAndLocaleMessagesExists_When_PassingMessageCodeDontExistsInList_Should_BeReturnThePassedCode(string locale)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);

            var messageCode = Fixture.Create<string>();

            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var messageFound = MessageCodesFactory.Instance.GetMessage(messageCode, Fixture.Create<string>());

            // Assert
            messageFound.Should().NotBeNullOrWhiteSpace();
            messageFound.Should().Be(messageCode);
        }

        [Theory]
        [InlineData("en-GB", "en-US")]
        [InlineData("en-GB", "pt-BR")]
        public void MessageCodesFactory_GetMessage_WithoutLocalePAramAndLocaleMessagesDontExists_When_PassingMessageCodeDontExistsInList_Should_BeReturnThePassedCode(string localeExecution, string localeParam)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo(localeExecution);

            var messageCode = Fixture.Create<string>();

            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var messageFound = MessageCodesFactory.Instance.GetMessage(messageCode, localeParam);

            // Assert
            messageFound.Should().NotBeNullOrWhiteSpace();
            messageFound.Should().Be(messageCode);
        }

        [Fact]
        public void MessageCodesFactory_GetMessage_WithLocale_When_PassingMessageCodeDontExistsInList_Should_BeReturnThePassedCode()
        {
            // Arrange
            var messageCode = Fixture.Create<string>();

            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var messageFound = MessageCodesFactory.Instance.GetMessage(messageCode, Fixture.Create<string>());

            // Assert
            messageFound.Should().NotBeNullOrWhiteSpace();
            messageFound.Should().Be(messageCode);
        }

        [Theory]
        [InlineData("pt-BR", "Message04", "Mensagem Compilada 04")]
        [InlineData("en-US", "Message04", "Builded Message #04")]
        [InlineData("pt-BR", "Message05", "Mensagem Compilada 05")]
        [InlineData("en-US", "Message05", "Builded Message #05")]
        [InlineData("pt-BR", "Message06", "Mensagem Compilada 06")]
        [InlineData("en-US", "Message06", "Builded Message #06")]
        public void MessageCodesFactory_GetMessage_WithLocaleFromBuildedResource_When_PassingMessageCode_Should_Be_ExpectedMessageContent(string locale, string messageCode, string message)
        {
            // Arrange
            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<MessageCodesTests>());

            // Act
            var messageFound = MessageCodesFactory.Instance.GetMessage(messageCode, locale);

            // Assert
            messageFound.Should().NotBeNullOrWhiteSpace();
            messageFound.Should().Be(message);
        }
    }

    //en-US
    public class LocaleMessages_EnUS
        : ILocaleMessages
    {
        public LocaleMessages_EnUS()
        {
            LocaleName = "en-US";

            Messages = new List<Message>
            {
                new Message { Code = Message01_EnUS.MyMessageCode, Content = Message01_EnUS.MyMessageContent },
                new Message { Code = Message02_EnUS.MyMessageCode, Content = Message02_EnUS.MyMessageContent },
                new Message { Code = Message03_EnUS.MyMessageCode, Content = Message03_EnUS.MyMessageContent }
            };
        }

        public string LocaleName { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message01_EnUS
        : IMessage
    {
        public const string MyMessageCode = "Message04";
        public const string MyMessageContent = "Builded Message #04";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }

    public class Message02_EnUS
    : IMessage
    {
        public const string MyMessageCode = "Message05";
        public const string MyMessageContent = "Builded Message #05";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }

    public class Message03_EnUS
        : IMessage
    {
        public const string MyMessageCode = "Message06";
        public const string MyMessageContent = "Builded Message #06";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }


    //pt-BR
    public class LocaleMessages_PtBR
        : ILocaleMessages
    {
        public LocaleMessages_PtBR()
        {
            LocaleName = "pt-BR";

            Messages = new List<Message>
            {
                new Message { Code = Message01_PtBR.MyMessageCode, Content = Message01_PtBR.MyMessageContent },
                new Message { Code = Message02_PtBR.MyMessageCode, Content = Message02_PtBR.MyMessageContent },
                new Message { Code = Message03_PtBR.MyMessageCode, Content = Message03_PtBR.MyMessageContent }
            };
        }

        public string LocaleName { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message01_PtBR
        : IMessage
    {
        public const string MyMessageCode = "Message04";
        public const string MyMessageContent = "Mensagem Compilada 04";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }

    public class Message02_PtBR
        : IMessage
    {
        public const string MyMessageCode = "Message05";
        public const string MyMessageContent = "Mensagem Compilada 05";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }

    public class Message03_PtBR
        : IMessage
    {
        public const string MyMessageCode = "Message06";
        public const string MyMessageContent = "Mensagem Compilada 06";

        public string Code { get; set; } = MyMessageCode;
        public string Content { get; set; } = MyMessageContent;
    }
}
