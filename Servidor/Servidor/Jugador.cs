using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    class Jugador
    {
        Usuario usuario;
        TcpClient cliente;

       public Jugador(Usuario usuario, TcpClient cliente)
        {
            this.usuario = usuario;
            this.cliente = cliente;
        }

       /* public float calcularPorcentaje()
        {
            //return (pGanadas * 100) / pJugadas;
        }
        public void buscarPartida()
        {

        }
        public void generarJugada()
        {

        }
        // metodo timer
        */
    }
}

