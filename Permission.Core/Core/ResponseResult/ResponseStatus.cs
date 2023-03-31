using System.ComponentModel;
using System.Reflection;

namespace Permission.Core.Core.ResponseResult
{
    public class ResponseStatus
    {
        public enum StatusMessages
        {
            [Description("El registro fue creado correctamente")]
            SuccessCreateMessage,
            [Description("El registro fue actualizado correctamente")]
            SuccessUpdateMessage,
            [Description("El registro fue eliminado correctamente")]
            SuccessDeleteMessage,
            [Description("El registro ya existe")]
            ErrorExistMessage,
            [Description("El registro no existe")]
            EntityNotFoundMessage,
            [Description("No se han encontrado resultados para la búsqueda")]
            NotFoundMessage,
            [Description("Ha ocurrido un error en la solicitud")]
            ErrorMessage,
            [Description("Se produjo una excepción al procesar su solicitud")]
            ErrorRequest,
            [Description("El estado del formulario no es válido")]
            ErrorModelState,
            [Description("Tiempo de espera agotado")]
            Timeout
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
