using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class TuristaPacote
    {

        public string Id { get; set; }
              
        public string IdTurista { get; set; }
              
        public string IdPacote { get; set; }

        public string StatusPedido { get; set; }

    }
}