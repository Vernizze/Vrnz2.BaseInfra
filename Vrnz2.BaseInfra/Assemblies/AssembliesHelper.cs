using System.Reflection;

namespace Vrnz2.BaseInfra.Assemblies
{
    public static class AssembliesHelper
    {
        #region Methods

        public static Assembly GetAssemblies() => typeof(AssembliesHelper).GetTypeInfo().Assembly;

        public static Assembly GetAssemblies<T>() => typeof(T).GetTypeInfo().Assembly;

        #endregion
    }
}
