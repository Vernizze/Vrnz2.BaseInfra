using Vrnz2.Infra.CrossCutting.Utils;

namespace Vrnz2.BaseInfra.Saga
{
    public class SagaHandler
        : ISagaHandler
    {
        #region Variables

        private readonly string _saga;

        #endregion

        #region Constructors

        public SagaHandler()
            =>_saga = CodeGenerator.Generate(20, "SG");

        #endregion

        #region Attributes

        public string GetSaga
            => _saga;

        #endregion
    }
}
