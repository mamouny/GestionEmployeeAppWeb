using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEmployeeAppWeb.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Nom est obligatoire*")]
        [Display(Name = "NOM")]
        public string EmpNom { get; set; }

        [Required(ErrorMessage = "Prenom est obligatoire*")]
        [Display(Name = "PRENOM")]
        public string EmpPrenom { get; set; }

        [Required(ErrorMessage = "Age est obligatoire*")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Departement est obligatoire*")]
        [Display(Name = "Departement")]
        public int DepId { get; set; }

        [NotMapped]
        [Display(Name = "Departement")]
        public string DepartementName { get; set; }

    }
}
