using ControlGastos.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ControlGastos.Api.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);            
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                "Administrador",
                "Usuario"
            };

            foreach (var nombreRol in roles)
            {
                if (await roleManager.RoleExistsAsync(nombreRol))
                {
                    continue;
                }

                var rol = new IdentityRole
                {
                    Name = nombreRol
                };

                var resultado = await roleManager.CreateAsync(rol);

                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(x => x.Description));

                    throw new Exception($"No se pudo crear el rol '{nombreRol}': {errores}");
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<Usuario> userManager)
        {
            const string email = "admin@email.com";
            const string password = "Admin123*";
            const string rol = "Administrador";

            var usuario = await userManager.FindByEmailAsync(email);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var resultado = await userManager.CreateAsync(usuario, password);

                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(x => x.Description));

                    throw new Exception($"No se pudo crear el usuario administrador: {errores}");
                }
            }

            if (!await userManager.IsInRoleAsync(usuario, rol))
            {
                var resultado = await userManager.AddToRoleAsync(usuario, rol);

                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(x => x.Description));
                    throw new Exception($"No se pudo asignar el rol '{rol}: {errores}'");
                }
            }
        }
    }
}
