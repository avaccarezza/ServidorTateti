using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Collections;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Iniciado servidor");

            TcpListener serverSocket = new TcpListener(8000);

            serverSocket.Start();
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                HandlerClient client = new HandlerClient();
                client.startClient(clientSocket);
            }

        }
    }

    public class HandlerClient
    {
        ArrayList  usuarios = new ArrayList();
        Usuario usuario;

        public void crearUsuarios()
        {
            usuarios.Add(new Usuario(1,"Raul","1234",800,10,5));
            usuarios.Add(new Usuario(2,"Jorge","1234",900,0,0));
            usuarios.Add(new Usuario(3,"Carlos","1234",1000,0,0));
        }


        TcpClient clientSocket;
        
        public void startClient(TcpClient clientSocket)
        {
           
            this.clientSocket = clientSocket;
            Thread threadClient = new Thread(doChat);
            threadClient.Start();

        }

        private void doChat()
        {
            
            
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            Usuario usuario = null;
            

            while (true)
            {
                byte[] bytesFrom = new byte[4];
               
                // Recibi mensaje
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                int buffersize = BitConverter.ToInt32(bytesFrom, 0);
                bytesFrom = new byte[buffersize];
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                
                if (dataFromClient.Contains("usuario"))
                {
                     usuario = JsonConvert.DeserializeObject<Usuario>(dataFromClient);
                     usuario.setConexion(clientSocket);
                }
                else
                {
                    recibirMensaje(dataFromClient, usuario);  
                }
            }
        }
        
        //enviar mensajes
        public void mensajeAlCliente(string mensaje)
        {
            Byte[] sendBytes = null;
            string serverResponse = null;

            
            serverResponse = mensaje;
            sendBytes = System.Text.Encoding.ASCII.GetBytes(serverResponse);
            byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Write(intBytes, 0, intBytes.Length);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

        public void recibirMensaje(string msj, Usuario usuario)
        {
            string mensaje = "";
            switch (msj)
            {
                case "c":
                    string rival = ListadoUsuarios.ponerEnCola(usuario);
                    if (rival != "")
                    {
                        mensaje = rival;
                        ListadoUsuarios.sacarDeLaCola(usuario);
                        usuario.setConexion(clientSocket);
                        mensajeAlCliente(mensaje);
                    }
                    break;
                case "d":
                    ListadoUsuarios.sacarDeLaCola(usuario);
                    mensaje = "saliste de la cola";
                    Console.WriteLine(mensaje);
                    break;
            }  
        }  
    }
}
