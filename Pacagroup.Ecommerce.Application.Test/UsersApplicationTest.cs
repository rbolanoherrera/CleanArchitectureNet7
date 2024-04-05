using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Services.WebApi;

namespace Pacagroup.Ecommerce.Application.Test
{
    [TestClass]
    public class UsersApplicationTest
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnviaParametros_RetornarMensajeErrorValidacion()
        {
            //Arrange: donde se inicializan los datos y/o objetos necesarios para la ejecución de la prueba
            
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            string username = string.Empty;
            string password = string.Empty;
            string expected = "Errores de Validación del objeto";

            //Act: donde se ejecuta la prueba y se obtiene el resultado
            var result = context.Authenticate(username, password);
            var actual = result.Message;

            //Assert: donde se comprueba que el resultado obtenido es el esperado
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_UsuarioNoExiste_RetornarNoExisteUsuario()
        {
            //Arrange: donde se inicializan los datos y/o objetos necesarios para la ejecución de la prueba

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            string username = "Pepito Perez";
            string password = "Ralfs.8310";
            string expected = "Usuario no existe";

            //Act: donde se ejecuta la prueba y se obtiene el resultado
            var result = context.Authenticate(username, password);
            var actual = result.Message;

            //Assert: donde se comprueba que el resultado obtenido es el esperado
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_ClaveInvalida_RetornarUsuarioNoExiste()
        {
            //Arrange: donde se inicializan los datos y/o objetos necesarios para la ejecución de la prueba

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            string username = "ralfs";
            string password = "123456";
            string expected = "Usuario no existe";

            //Act: donde se ejecuta la prueba y se obtiene el resultado
            var result = context.Authenticate(username, password);
            var actual = result.Message;

            //Assert: donde se comprueba que el resultado obtenido es el esperado
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_AutenticacionExitosa_RetornarAutenticacionExitosa()
        {
            //Arrange: donde se inicializan los datos y/o objetos necesarios para la ejecución de la prueba

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            string username = "ralfs";
            string password = "ralfs.8310";
            string expected = "Autenticación de Usuario exitosa";

            //Act: donde se ejecuta la prueba y se obtiene el resultado
            var result = context.Authenticate(username, password);
            var actual = result.Message;

            //Assert: donde se comprueba que el resultado obtenido es el esperado
            Assert.AreEqual(expected, actual);
        }

    }
}