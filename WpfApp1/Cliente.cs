using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Cliente 
    {
        //DATOS CLIENTE
        string nombreCli;
        string apellidoCli;
        string dniCli;
        string clientSucur;
        string ubicacion;


        //DATOS DE CUENTA
        string cuentaCli;
        int dineroCuenta;

        public string NombreCli { get => nombreCli; set => nombreCli = value; }
        public string ApellidoCli { get => apellidoCli; set => apellidoCli = value; }
        public string DniCli { get => dniCli; set => dniCli = value; }
        public string CuentaCli { get => cuentaCli; set => cuentaCli = value; }

        public string ClientSucur { get => clientSucur; set => clientSucur = value; }
        public int DineroCuenta { get => dineroCuenta; set => dineroCuenta = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }

        public Cliente(string ciudad, string ubicacion, string cp, string nombre, string apellido, string dni, int numcliente) 
        {

        this.nombreCli = nombre;
        this.apellidoCli = apellido;
        this.dniCli = dni;
        this.clientSucur = ciudad; //el nombre de la sucursal asociada al cliente
        this.Ubicacion= ubicacion;

        //AL CREAR CLIENTE- GENERO UNA NUEVA CUENTA,E ESTA MISMA HEREDA LOS DATOS DE LA SUCURSAL SELECCIONADA
        Cuenta nueva = new Cuenta(ciudad, ubicacion, cp, numcliente);  //AL CREAR NUEVA CUENTA TENDRÉ TODOS LOS DATOS DE LAS SUCURSALES, PORQUE LO HEREDA
        this.cuentaCli = nueva.CuentaCod;
        this.DineroCuenta = nueva.Dinero;
        



        }
    }
}
