using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICCompatition.Models
{
    public class ExerciseViewModel
    {
        [Required(ErrorMessage = "Id not exist")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the correct Name")]
        [MaxLength(100, ErrorMessage = "Your text is too long")]
        public String ExerciseName { get; set; }

        [Required(ErrorMessage = "Please enter the correct Date")]
        [RegularExpression("([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8])))")]
        public DateTime ExerciseDateTime { get; set; }

        [Required(ErrorMessage = "Please enter the time range")]
        [Range(1, 120, ErrorMessage = "Please enter the time range from 1 ~ 120")]
        public int DurationInMinutes { get; set; }
        
    }
}