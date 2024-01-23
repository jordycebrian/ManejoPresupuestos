using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ManejoPresupuesto.Models
{
    public class TiposCuentas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarSiExisteTipoCuenta", controller:"TiposCuentas", AdditionalFields = nameof(Id))]
        public string Nombre { get; set; }
        public int UsuarioId {  get; set; }
        public int Orden {  get; set; }

    }
}
