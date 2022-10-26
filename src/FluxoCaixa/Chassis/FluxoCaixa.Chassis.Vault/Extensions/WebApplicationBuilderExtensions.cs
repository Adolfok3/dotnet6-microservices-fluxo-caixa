using FluxoCaixa.Chassis.Utils.Helpers;
using Microsoft.AspNetCore.Builder;
using VaultSharp.Extensions.Configuration;

namespace FluxoCaixa.Chassis.Vault.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddVault(this WebApplicationBuilder builder)
        {
            var vaultAddress = RequireEnvironmnetVariable("VAULT_ADDRESS");
            var vaultToken = RequireEnvironmnetVariable("VAULT_TOKEN");
            var serviceName = new AssemblyHelper().GetServiceName();

            builder.Configuration.AddVaultConfiguration(() => new VaultOptions(vaultAddress, vaultToken), "Global", "FluxoCaixa");
            builder.Configuration.AddVaultConfiguration(() => new VaultOptions(vaultAddress, vaultToken), serviceName, "FluxoCaixa");
        }

        private static string RequireEnvironmnetVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable)
                               ?? throw new ArgumentException($"Variable {variable} is required");
        }
    }
}
