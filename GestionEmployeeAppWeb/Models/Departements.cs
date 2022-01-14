using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEmployeeAppWeb.Models
{
    public class Departements
    {
        [Key]
        public int DepId { get; set; }
        [Required(ErrorMessage = "Le nom du departement est obligatoire*")]
        [Display(Name = "NOM DEPARTEMENT")]
        public string DepartementName { get; set; }
    }
}
