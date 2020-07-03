using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class LogicaJuego
    {

        public int[] tablero = new int[9];
        static bool player = true;

        public LogicaJuego()
        {

        }


        public void tableroDefault()
        {
            for (int i = 0; i <= 8; i++)
            {
                tablero[i] = 0;
                Console.WriteLine(" " + tablero[i]);

            }
        }


        public void terminaPartida()
        {

        }
        public String ponerFicha()
        {

            if (player)
            {
                player = false;
                return "X";
            }
            else
            {
                // tablero[i] = 'O';
                player = true;
                return "O";
            }


        }
    }
}
