using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using System.Reflection;

namespace FluxoCaixa.Chassis.Utils.Helpers
{
    public class AssemblyHelper : IAssemblyHelper
    {
        public string GetServiceName()
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            return assemblyName?.Split(".").Last();
        }
    }
}
