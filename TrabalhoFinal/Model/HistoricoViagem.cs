using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HistoricoViagem
    {
        public int Id { get; set; }
 
        [DataType(DataType.Date)]
        
        public DateTime Data { get; set; }

        public string DataPadraoBR { get { return string.Format("{0:dd/MM/yyyy}", Data);  } }

        public Pacote Pacote { get; set; }

        public int IdPacote { get; set; }
    }
}
