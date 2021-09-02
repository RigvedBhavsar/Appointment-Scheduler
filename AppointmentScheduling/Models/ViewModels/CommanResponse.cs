using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Models.ViewModels
{
    public class CommanResponse<T>
    {
        public int status { get; set; }
        public String message { get; set; }
        public T dataenum { get; set; }
    }
}
