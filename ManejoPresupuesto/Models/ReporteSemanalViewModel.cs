


namespace ManejoPresupuesto.Models
{
    public class ReporteSemanalViewModel
    {
        public decimal Ingresos => TransaccionPorSemana.Sum(x => x.Ingresos);
        public decimal Gastos => TransaccionPorSemana.Sum(x => x.Gastos);
        public decimal Total => Ingresos - Gastos;
        public DateTime FechaReferencia { get; set; }
        public IEnumerable<ResultadoObtenerPorSemana> TransaccionPorSemana { get; set; }
    }
}
