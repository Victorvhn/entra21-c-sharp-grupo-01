﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PacotePontoTuristico
    {
        public int Id{ get; set; }

        public PontoTuristico PontoTuristoco{ get; set; }

        public int IdPontoTuristico { get; set; }

        public Pacote Pacote { get; set; }

        public int IdPacote { get; set; }
    }
}
