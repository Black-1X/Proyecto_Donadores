
namespace Sales.API.Constanst
{
   // se uso la misma que hice en clase :D
public static class HttpStatusCode
{
    // La solicitud se realizó correctamente
    public const int OK = 200;

    // El recurso solicitado no fue encontrado
    public const int NotFound = 404;

    // El recurso fue creado exitosamente
    public const int Created = 201;

    // La solicitud se procesó correctamente y no devuelve contenido
    public const int NoContent = 204;

    // La solicitud contiene datos incorrectos o inválidos
    public const int BadRequest = 400;

    // El usuario no está autenticado o las credenciales son inválidas
    public const int Unauthorized = 401;

    // El usuario está autenticado pero no tiene permisos para acceder al recurso
    public const int Forbidden = 403;

    // Existe un conflicto con el estado actual del recurso
    public const int Conflict = 409;

    // La solicitud es válida pero contiene errores semánticos o de validación
    public const int UnprocessableEntity = 422;

    // Error interno del servidor
    public const int InternalServerError = 500;

    // La funcionalidad solicitada aún no ha sido implementada
    public const int NotImplemented = 501;

    // El servicio no está disponible temporalmente
    public const int ServiceUnavailable = 503;
}
}