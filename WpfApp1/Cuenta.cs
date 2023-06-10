using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Banco
{
    internal class Cuenta : Sucursal
    {
        int dinero = 50;
        string cuentaCod;
        string[] totalCuentasCod = new string[100]; //100 porque en total son 100 clientes

        string ciudadSucur;
        string ubiSucur;
        string cpSucur;



        public Cuenta(string ciudad, string ubicacion, string cp, int CantCuentas) :  base(ciudad, ubicacion, cp)
        { 
        
        this.CuentaCod = GenerarCodigoCuenta();
            TotalCuentasCod[CantCuentas] = CuentaCod; //INSERTO EL CODIGO EN LA POSICION DE LISTA

            //COMPROBACION DE CODIGOS DIFERENTES!
           for (int i = 0; i< CantCuentas+1; i++) //RECORRO EL ARRAY DE CODIGOS DE CUENTA 
            {
                if (i != CantCuentas) // CantCuentas será el ultimo codigo agregado a la lista
                {
                    if (TotalCuentasCod[CantCuentas] == TotalCuentasCod[i])// SI EL CODIGO CREADO ES IGUAL AL ANTERIOR 
                    {
                        this.CuentaCod = GenerarCodigoCuenta(); //creo un nuevo codigo
                    }
                }
            }

        }

        public int Dinero { get => dinero; set => dinero = value; }
        public string CuentaCod { get => cuentaCod; set => cuentaCod = value; }
        public string[] TotalCuentasCod { get => totalCuentasCod; set => totalCuentasCod = value; }
        public string CiudadSucur { get => ciudadSucur; set => ciudadSucur = value; }
        public string UbiSucur { get => ubiSucur; set => ubiSucur = value; }
        public string CpSucur { get => cpSucur; set => cpSucur = value; }

        public string GenerarCodigoCuenta()
        {
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
