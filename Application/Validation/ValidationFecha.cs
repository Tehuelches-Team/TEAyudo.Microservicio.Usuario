using Application.Exceptions;

namespace Application.Validation
{
    public static class ValidationFecha
    {
        public static DateTime Validar(string Fecha)
        {
            if (Fecha.Contains("/"))
            {
                throw new ExceptionFecha("La fecha se debe ingresar con \"-\" no con \"/\".");
            }
            DateTime FechaParse;
            if (!DateTime.TryParse(Fecha, out FechaParse))
            {
                throw new ExceptionFecha("La fecha no se ingreso en un formato valido.");
            }
            else
            {
                FechaParse = FechaParse.Date;
                return FechaParse;
            }
        }
    }
}
