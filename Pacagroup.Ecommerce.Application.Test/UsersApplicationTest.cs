using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;

namespace Pacagroup.Ecommerce.Application.Test
{
    [TestClass]
    public class UsersApplicationTest
    {
        private static WebApplicationFactory<Program> _factory = null;
        private static IServiceScopeFactory _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            _factory = new WebApplicationFactory<Program>();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
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