using System.ComponentModel;

namespace BSX.FG.Ejemplo.Cliente.Base
{
    /// <summary>
    /// Códigos de error por defecto para la clase de resultados
    /// </summary>
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// Sin error
        /// </summary>
        [Description("NONE")]
        NONE = 0,

        /// <summary>
        /// Error en la lógica de negocios. Revise su entrada y los mensajes de error
        /// </summary>
        [Description("LOGIC_ERROR")]
        LOGIC_ERROR = 1,

        /// <summary>
        /// La base de datos no ha aceptado los valores enviados. Comuníquese con el administrador
        /// </summary>
        [Description("DB_ERROR")]
        DB_ERROR = 2,

        /// <summary>
        /// No hay datos disponibles. Revise los parámetros y la forma como los esta enviando
        /// </summary>
        [Description("NO_DATA")]
        NO_DATA = 3,

        /// <summary>
        /// Error relacionado con el API KEY
        /// </summary>
        [Description("APIKEY_ERROR")]
        APIKEY_ERROR = 4,

        /// <summary>
        /// No se han proporcionado datos para autorizar la operación
        /// </summary>
        [Description("MISSING_AUTHORIZATION")]
        MISSING_AUTHORIZATION = 5,

        /// <summary>
        /// Las credenciales proporcionadas son inválidas
        /// </summary>
        [Description("INVALID_CREDENTIALS")]
        INVALID_CREDENTIALS = 6,

        /// <summary>
        /// No se ha podido realizar la consulta solicitada
        /// </summary>
        [Description("QUERY_ERROR")]
        QUERY_ERROR = 7,

        /// <summary>
        /// El servicio que se esta consultando no esta disponible o ha ocurrido un error al hacer la llamada
        /// </summary>
        [Description("SERVICE_UNAVAILABLE")]
        SERVICE_UNAVAILABLE = 8,

        /// <summary>
        /// Un elemento requerido de los hedaers no esta presente
        /// </summary>
        [Description("MISSING_HEADER")]
        MISSING_HEADER = 9,

        /// <summary>
        /// Error no controlado en algun proceso de la aplicación
        /// </summary>
        [Description("APPLICATION_ERROR")]
        APP_ERROR = 10,

        /// <summary>
        /// El error esta del lado del usuario
        /// </summary>
        [Description("USER_ERROR")]
        USER_ERROR = 11,

        /// <summary>
        /// El servicio esta suspendido
        /// </summary>
        [Description("VIGENCY_ERROR")]
        SERVICE_SUSPENDED = 12,

        /// <summary>
        /// Elemento no existe o no ha sido encontrado
        /// </summary>
        [Description("NOT_FOUND")]
        NOT_EXISTS = 13,

        /// <summary>
        /// Error en la entrada proporcionada por el usuario
        /// </summary>
        [Description("INPUT_ERROR")]
        INPUT_ERROR = 14,

        /// <summary>
        /// Error en la validación de los datos
        /// </summary>
        [Description("VALIDATION_ERROR")]
        VALIDATION_ERROR = 15,

        /// <summary>
        /// Ya no hay elementos qué consumir en la cuenta
        /// </summary>
        [Description("VALIDATION_ERROR")]
        ITEMS_DEPLETED = 16,

        /// <summary>
        /// Hay errores en la cuenta que deben ser corregidos
        /// </summary>
        [Description("ACCOUNT_ERROR")]
        ACCOUNT_ERROR = 17
    }
}