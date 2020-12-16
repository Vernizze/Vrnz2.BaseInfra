using Microsoft.Extensions.Configuration;

namespace Vrnz2.BaseInfra.Settings
{
    public class ConfigurationFactory
    {
        #region Variables

        private static ConfigurationFactory _instance;

        #endregion

        #region Constructors

        private ConfigurationFactory() { InitConfiguration(); }

        #endregion

        #region Attributes

        public static ConfigurationFactory Instance
        {
            get
            {
                _instance = _instance ?? new ConfigurationFactory();

                return _instance;
            }
        }

        public IConfiguration Configuration { get; private set; }

        #endregion

        #region Methods

        private void InitConfiguration()
            => Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        #endregion
    }
}
