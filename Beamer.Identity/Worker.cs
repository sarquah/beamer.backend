using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Beamer.Identity.Models;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Beamer.Identity
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("Beamer.API") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Beamer.API",
                    ClientSecret = "ce7f551d-34d9-4eec-93aa-83f9a6a6c48c",
                    DisplayName = "Beamer API",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                });
            }

            if (await manager.FindByClientIdAsync("Beamer.client") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Beamer.client",
                    ClientSecret = "88456544-1c45-49fa-bc87-91c19976655b",
                    DisplayName = "Beamer Client",
                    PostLogoutRedirectUris =
                    {
                        new Uri("http://localhost:65100/signout-oidc")
                    },
                    RedirectUris =
                    {
                        new Uri("http://localhost:65100/signin-oidc")
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.GrantTypes.Implicit,
                        Permissions.ResponseTypes.IdToken,
                        Permissions.ResponseTypes.IdTokenToken,
                        Permissions.ResponseTypes.Token,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles
                    }
                });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
