using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Banco
{
    public class Sucursal : Window
    {
        
        string sucursalCod;
        string sucursalCity;
        string sucursalLocation;
        string sucurCP;
        string[] totalSucursalCod = new string[5]; //5 sucursales
        int posicion;



        public string SucursalCod { get => sucursalCod; set => sucursalCod = value; }
        public string SucursalCity { get => sucursalCity; set => sucursalCity = value; }
        public string SucursalLocation { get => sucursalLocation; set => sucursalLocation = value; }
        public string SucurCP { get => sucurCP; set => sucurCP = value; }



        public Sucursal(string ciudad, string ubicacion, string cp)
        {
                this.sucursalCity = ciudad;
                this.SucursalLocation = ubicacion;
                this.sucurCP = cp;
                this.SucursalCod = GenerarCodigoSucursal();


            //AGREGARLO A LA ULTIMA POSICION DE LA LISTA
            for (int i = 0; i < totalSucursalCod.Length; i++) //RECORRO EL ARRAY DE CODIGOS DE CUENTA 
            {
                if (totalSucursalCod[i] == null) //AGREGANDO AL PRIMER VACIO
                { 
                    totalSucursalCod[i] = this.SucursalCod;
                    posicion = i;
                    break;
                }
            }

            //COMPROBACION DE CODIGOS DIFERENTES!
            for (int i = 0; i < posicion; i++) //RECORRO EL ARRAY SIN CONTARLA ULTIMAPOSICION 
            {
                if (totalSucursalCod[i] == this.SucursalCod)// SI EL CODIGO CREADO ES IGUAL AL ANTERIOR 
                  {
                    this.SucursalCod = GenerarCodigoSucursal(); //creo un nuevo codigo
                    totalSucursalCod[posicion] = SucursalCod;
                }
                
            }

        }

        


            public string GenerarCodigoSucursal() {         
            //Posicion devolverá el codigo asociado a la posicion solicitada

            Random generador = new Random();

            int aleatorio;

            //Creamos una lista que tendrá todos los numeros Y Generará un codigo
            int[] codList = new int[4];
            for (int i = 0; i < codList.Length; i++)
            {
                aleatorio = generador.Next(1, 10); // el ultimo no lo cuenta
                codList[i] = aleatorio;  // por cada iteración agregará un numero [][][][]

            };

            //GUARDAMOS NUMERO ALEATORIO EN VARIABLE CUENTA
            string NuevoCodigo = codList[0].ToString() + codList[1].ToString() + codList[2].ToString() + codList[3].ToString();
            return NuevoCodigo;
           
        }

           



        }



    }
