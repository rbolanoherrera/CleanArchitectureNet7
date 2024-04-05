namespace Pacagroup.Ecommerce.Infrastructure.EventBus.Options
{
    /// <summary>
    /// init en c# 9 o superiores permite inmutabilidad, es decir cuando se crea el objeto 
    /// se inicializa el valor pero despues no se puede cambiar
    /// </summary>
    public class RabbitMQOptions
    {
        public string HostName { get; init; }
        public string VirtualHost { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}