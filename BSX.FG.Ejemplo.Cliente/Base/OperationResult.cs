using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BSX.FG.Ejemplo.Cliente.Base
{
    /// <summary>
    /// Tipo de mensaje registrado en la información del proceso
    /// </summary>
    public enum MessageTypeEnum
    {
        /// <summary>
        /// Información
        /// </summary>
        [Description("Información")]
        Info,

        /// <summary>
        /// Error
        /// </summary>
        [Description("Error")]
        Error,

        /// <summary>
        /// Alerta relevante
        /// </summary>
        [Description("Alerta")]
        Warning,

        /// <summary>
        /// Éxito
        /// </summary>
        [Description("Éxito")]
        Success
    }

    /// <summary>
    /// Indica el tipo de resultado que se ha obtenido en el proceso
    /// </summary>
    public enum ResultTypeEnum
    {
        /// <summary>
        /// La operación se ha completado exitosamente
        /// </summary>
        [Description("Éxito")]
        Success,

        /// <summary>
        /// El resultado es informativo
        /// </summary>
        [Description("Información")]
        Informative,

        /// <summary>
        /// El proceso ha resultado con fallas
        /// </summary>

        [Description("Error")]
        Error
    }

    /// <summary>
    /// Mensaje obtenido
    /// </summary>
    public class OperationMessage
    {
        /// <summary>
        /// Constructor principal
        /// </summary>
        public OperationMessage()
        {
        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        /// <param name="text">Texto de la operación</param>
        /// <param name="messageType">Tipo de mensaje.</param>
        public OperationMessage(string text, MessageTypeEnum messageType)
        {
            Text = text;
            MessageType = messageType;
        }

        /// <summary>
        /// Tipo de mensaje
        /// </summary>
        public MessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// Nombre del tipo de mensaje
        /// </summary>
        public string MessageTypeName => MessageType.ToName();

        /// <summary>
        /// Texto del mensaje
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sobrecarga de la impresión del objeto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{MessageType.ToName()} - {Text}";
        }
    }

    public class OperationResult
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Código de error para mostrar al usuario
        /// </summary>
        public ErrorCodeEnum ErrorCode { get; private set; } = ErrorCodeEnum.NONE;

        /// <summary>
        /// Descripción del código de error
        /// </summary>
        public string ErrorCodeName => ErrorCode.ToName();

        /// <summary>
        /// Información obtenida durante el proceso
        /// </summary>
        public List<OperationMessage> Info { get; private set; } = new List<OperationMessage>();

        /// <summary>
        /// Lista de mensajes generados al presentar los resultados
        /// </summary>
        public IEnumerable<string> Messages => Info.Select(x => x.Text).ToList();

        /// <summary>
        /// Resultado obtenido en la operación
        /// </summary>
        public ResultTypeEnum Result { get; private set; } = ResultTypeEnum.Success;

        /// <summary>
        /// Mensajes mostrados en una línea única
        /// </summary>
        public string SingleLineErrors => String.Join(", ", Messages);

        /// <summary>
        /// Indica si la operación a tratar ha sido exitosa o no
        /// </summary>
        public bool Success => Result == ResultTypeEnum.Success;

        /// <summary>
        /// Agrega un mensaje a la lista de mensajes y lo escribe en el log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mtype"></param>
        public void AddMessage(string message, MessageTypeEnum mtype = MessageTypeEnum.Info)
        {
            Info.Add(new OperationMessage(message, mtype));
            log.Info(message);
        }

        /// <summary>
        /// Agrega un grupo de mensajes a la información de resultado
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="mtype"></param>
        public void AddMessages(IEnumerable<string> messages, MessageTypeEnum mtype = MessageTypeEnum.Info)
        {
            Info.AddRange(messages.Select(x => new OperationMessage(x, mtype)));
        }

        /// <summary>
        /// Muestra los mensajes en la consola
        /// </summary>
        public void DisplayToConsole()
        {
            Console.WriteLine($"Resultado: {Success}");
            Console.WriteLine($"Tipo de resultado: {Result.ToName()}");
            foreach (var item in Info)
            {
                Console.WriteLine($"{item.MessageType.ToName()} - {item.Text}");
            }
        }

        /// <summary>
        /// Marca como inválido el operation result y escribe en el log la excepción. No agrega contenido a los mensajes
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="notes"></param>
        public void LogException(Exception ex, string notes = "")
        {
            Result = ResultTypeEnum.Error;

            if (!string.IsNullOrEmpty(notes))
                log.Error(notes);

            log.Error(ex);
        }

        /// <summary>
        /// Marca el resultado como fallido
        /// </summary>
        /// <param name="result">Objeto de resultados de donde se sacan los mensajes</param>
        public void MarkAsFailed(OperationResult result)
        {
            Result = result.Result;

            Info.AddRange(result.Info);
            ErrorCode = result.ErrorCode;
        }

        /// <summary>
        /// Marca el resultado como fallido
        /// </summary>
        /// <param name="message">Mensaje de error a ingresar a la lista</param>
        /// <param name="errorCode">Código de error</param>
        public void MarkAsFailed(string message = "Ha ocurrido un error y el proceso no ha podido continuar", ErrorCodeEnum errorCode = ErrorCodeEnum.LOGIC_ERROR)
        {
            Result = ResultTypeEnum.Error;

            Info.Add(new OperationMessage(message, MessageTypeEnum.Error));
            log.Error(message);
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Marca el resultado como fallido
        /// </summary>
        /// <param name="messages">Lista de mensajes a ingresar</param>
        /// <param name="errorCode">Código de error</param>
        public void MarkAsFailed(IEnumerable<string> messages, ErrorCodeEnum errorCode = ErrorCodeEnum.LOGIC_ERROR)
        {
            if (messages == null)
                messages = new string[] { "Se recomienda hacer una verificación mas profunda, la lista de errores llego nula" };

            Result = ResultTypeEnum.Error;

            Info.AddRange(messages.Select(x => new OperationMessage(x, MessageTypeEnum.Error)));
            ErrorCode = errorCode;

            foreach (var item in messages)
                log.Error(item);
        }

        /// <summary>
        /// Marca el resultado como fallido
        /// </summary>
        /// <param name="ex">Excepción que genera el error. Toma en cuenta el error interno si es que este existe</param>
        /// <param name="errorCode">Código de error</param>
        public void MarkAsFailed(Exception ex, ErrorCodeEnum errorCode = ErrorCodeEnum.LOGIC_ERROR)
        {
            Result = ResultTypeEnum.Error;

            Info.Add(new OperationMessage("Ha ocurrido una excepción no controlada, ha quedado registrada en el log de la plataforma.", MessageTypeEnum.Error));

            log.Error(ex);
            ErrorCode = errorCode;
            if (ex.InnerException != null)
            {
                log.Error(ex.InnerException, "Inner Exception");
            }
        }

        /// <summary>
        /// Fija el código de error
        /// </summary>
        /// <param name="code">Código de error</param>
        public void SetErrorCode(ErrorCodeEnum code)
        {
            ErrorCode = code;
        }

        /// <summary>
        /// Descripción del objeto
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Success} - {Info.Count} mensajes. Código de error: {ErrorCode.ToName()}";

        /// <summary>
        /// Escribe los resultados en el log a nivel de DEBUG
        /// </summary>
        public void WriteToDebugLog()
        {
            log.Debug($"• Resultado: {Success}");
            foreach (var item in Info)
            {
                log.Debug($"{item.MessageType.ToName()} - {item.Text}");
            }
        }
    }

    /// <summary>
    /// Resultado de la operación que contiene un resultado dentro
    /// </summary>
    /// <typeparam name="T">Tipo del resultado</typeparam>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Resultado con contenido
        /// </summary>
        public OperationResult() : base()
        { }

        /// <summary>
        /// Resultado con contenido
        /// </summary>
        /// <param name="content">Objeto de resultado</param>
        public OperationResult(T content) : base()
        {
            Content = content;
        }

        /// <summary>
        /// Contenido. Solo se puede leer
        /// </summary>
        public T Content { get; private set; }

        /// <summary>
        /// Muestra el contenido del objeto en la consola
        /// </summary>
        public new void DisplayToConsole()
        {
            base.DisplayToConsole();
            if (Content != null)
                Console.WriteLine($"Contenido: {Content}");
        }

        /// <summary>
        /// Fija el contenido de la operación
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(T content) => Content = content;
    }
}