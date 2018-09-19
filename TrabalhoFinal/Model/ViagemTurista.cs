using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViagemTurista
    {
        public int Id { get; set; }

        public  Turista Turista { get; set; }

        public Viagem Viagem { get; set; }

        public int IdTurista { get; set; }

        public int IdViagem { get; set; }

        public float Valor { get; set; }

    }
}
