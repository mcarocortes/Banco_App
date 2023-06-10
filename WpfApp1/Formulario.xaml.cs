using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

//
// NOMBRE: Macarena Caro
// Curso: 1º DAW
// Modalidad Presencial
// Práctica nº 4
//


namespace Banco
{

    public partial class Formulario : Window
    {
        Button botones;
        TextBox textos;
        MenuItem menuItem;

        int CantidadSucursal = 0; //cantidad actual
        int CantidadClientes = 0; //cantidad actual
        int CantidadMaxClientes = 20;

        Sucursal[] sucursalesTotales = new Sucursal[5]; //CREAMOS UN ARRAY DE 5 SUCURSALES 
        Cliente[] clientesTotales = new Cliente[100];// MAX 20 X SUCURSAL// 20x5=100  



        public Formulario()
        {

            InitializeComponent();


            //*********** CREANDO SUCURSAL POR DEFECTO          
            sucursalesTotales[CantidadSucursal] = new Sucursal("SANTIAGO", "MARINA 77", "11111");
            
            //*********** CREANDO CLIENTE POR DEFECTO - CORRESPONDIENTE A LA SUCURSAL POR DEFECTO
            clientesTotales[CantidadClientes] = new Cliente(sucursalesTotales[CantidadSucursal].SucursalCity, sucursalesTotales[CantidadSucursal].SucursalLocation, sucursalesTotales[CantidadSucursal].SucurCP, "MACARENA", "CARO", "Y768756G", CantidadClientes);


            //---- WINDOW VISTA DE BANCO: SUCURSALES
            Combo_Sucursales2.Items.Insert(CantidadSucursal, sucursalesTotales[CantidadSucursal].SucursalCity);
            //---- WINDOW VISTA DE BANCO: CUENTAS
            List_Cuentas.Items[CantidadClientes] = " " + clientesTotales[CantidadClientes].CuentaCli + "   |   " + clientesTotales[CantidadClientes].DineroCuenta + "€" + "   |   " + clientesTotales[CantidadClientes].NombreCli + " | " + clientesTotales[CantidadClientes].ClientSucur;
            //---- WINDOW VISTA DE BANCO: CLIENTES
            List_Clientes.Items[CantidadClientes] = clientesTotales[CantidadClientes].NombreCli + " | " + clientesTotales[CantidadClientes].ApellidoCli + " | " + clientesTotales[CantidadClientes].DniCli + " | " + clientesTotales[CantidadClientes].CuentaCli + " | " + clientesTotales[CantidadClientes].ClientSucur;

            //---- WINDOW FORMULARIO: CLIENTE
            Combo_Sucursales.Items.Insert(CantidadSucursal, sucursalesTotales[CantidadSucursal].SucursalCity);

            CantidadClientes++;
            CantidadSucursal++;
        }

        //EXCEPCION PARA INGRESO DEL IMPORTE, DEBEN SER NUMEROS:
        private void textblock_text(object sender, TextChangedEventArgs e)
        {
            textos = sender as TextBox;
            try
            {
                switch (textos.Name)
                { case "Text_MontoIngreso": Convert.ToInt32(Text_MontoIngreso.Text); break;
                    case "Text_DNI": if (Text_DNI.Text == "" || Text_DNI.Text == null) { DIGITOS.Visibility = Visibility.Visible; } else { DIGITOS.Visibility = Visibility.Hidden; } break;
                    case "Text_CPSuc": if (Text_CPSuc.Text == "" || Text_CPSuc.Text == null) { DIGITOS2.Visibility = Visibility.Visible; } else { DIGITOS2.Visibility = Visibility.Hidden; } break;

                }
            }
            catch
            {Text_MontoIngreso.Clear();
            }
        }

        //METODO PARA AGREGAR CLIENTES O SUCURSALES CON EL BOTÓN EN WINDOWS FORMULARIO
        private void Click_Alta(object sender, RoutedEventArgs e)
        {
            botones = sender as Button;
            switch (botones.Name)
            {
                //-------------- PARA CREAR SUCURSAL
                case "CrearSuc":
                    //SI LOS CAMPOS ESTÁN VACIOS, NO SE CREARÁ SUCURSAL HASTA TENER TODO
                    if ( (Text_CiudadSuc.Text == "" || Text_CiudadSuc.Text == null) ||(Text_UbiSuc.Text == "" || Text_UbiSuc.Text == null) ||(Text_CPSuc.Text == "" || Text_CPSuc.Text == null || Text_CPSuc.Text.Length < 5))
                    {
                        MessageBox.Show("Debe rellenar todos los campos");
                    }
                    else
                    {
                        // *****AGREGAR UNA NUEVA SUCURSAL LA LISTA DE SUCURSALES     
                        sucursalesTotales[CantidadSucursal] = new Sucursal(Text_CiudadSuc.Text.ToUpper(), Text_UbiSuc.Text.ToUpper(), Text_CPSuc.Text.ToUpper()); //ESPACIO EN LA LISTA                                                                                                                        
                        //MOSTRAR CODIGO
                        Text_CodSucu.Text = sucursalesTotales[CantidadSucursal].SucursalCod;
                        MostrarCodigo.Visibility = Visibility.Visible;

                        MessageBox.Show(
                         "NUEVA SUCURSAL Nº" + (CantidadSucursal + 1) + ": " +
                         "\nCiudad:" + sucursalesTotales[CantidadSucursal].SucursalCity +
                         "\nUbicación:" + sucursalesTotales[CantidadSucursal].SucursalLocation +
                         "\nCP: " + sucursalesTotales[CantidadSucursal].SucurCP +
                         "\nCodigo: " + sucursalesTotales[CantidadSucursal].SucursalCod);

                        //---- WINDOW FORMULARIO: CLIENTE
                        Combo_Sucursales.Items.Insert(CantidadSucursal, sucursalesTotales[CantidadSucursal].SucursalCity);
                        //---- WINDOW VISTA BANCO: SUCURSALES
                        Combo_Sucursales2.Items.Insert(CantidadSucursal, sucursalesTotales[CantidadSucursal].SucursalCity);

                        CantidadSucursal++;
                        
                        Text_CiudadSuc.Clear();
                        Text_UbiSuc.Clear();
                        Text_CPSuc.Clear();
                        MostrarCodigo.Visibility = Visibility.Hidden;

                        //-----------VERIFICACIONES -----------------
                        // *****VERIFICANDO CANTIDAD DE SUCURSALES
                        if (CantidadSucursal >= sucursalesTotales.Length)
                        {
                            //CUANDO YA ESTÉN TODAS LAS SUCURSALES SE BLOQUEARÁN LOS CAMPOS
                            MessageBox.Show("Has llegado al Límite de Sucursales");
                            Text_CiudadSuc.IsEnabled = false;
                            Text_UbiSuc.IsEnabled = false;
                            Text_CPSuc.IsEnabled = false;
                            Text_CodSucu.IsEnabled = false;
                        }
                    }
                    break;

                //-------------- PARA CREAR CLIENTE
                case "Alta":
                    //SI LOS CAMPOS ESTÁN VACIOS  NO SE CREARÁ CLIENTE, HASTA TENER TODO
                    if ( (Text_Nombre.Text == "" || Text_Nombre.Text == null) ||(Text_Apellido.Text == "" || Text_Apellido.Text == null) ||(Text_DNI.Text == "" || Text_DNI.Text == null || Text_DNI.Text.Length < 8 ) ||
                       (Combo_Sucursales.SelectedIndex == -1) || (Combo_Sucursales.Text == "---")  )
                    {
                        MessageBox.Show("Debe rellenar todos los campos");
                    }
                    else
                    { 
                        // VINCULO LOS DATOS CON CLIENTE CON LA SUCURSAL SELECCIONADA:
                        clientesTotales[CantidadClientes] = new Cliente(sucursalesTotales[Combo_Sucursales.SelectedIndex].SucursalCity, sucursalesTotales[Combo_Sucursales.SelectedIndex].SucursalLocation, sucursalesTotales[Combo_Sucursales.SelectedIndex].SucurCP,
                         Text_Nombre.Text.ToUpper(), Text_Apellido.Text.ToUpper(), Text_DNI.Text.ToUpper(),CantidadClientes);

                        //MOSTRAR CODIGO
                        Text_Cuenta.Text = clientesTotales[CantidadClientes].CuentaCli;
                        MostrarCuenta.Visibility = Visibility.Visible;

                        
                        //MUESTRO LOS DATOS
                       MessageBox.Show(
                        "\nCLIENTE DE LA SUCURSAL: " + clientesTotales[CantidadClientes].ClientSucur +
                        "\n\nNOMBRE:" + clientesTotales[CantidadClientes].NombreCli +
                        "\nAPELLIDO:" + clientesTotales[CantidadClientes].ApellidoCli +
                        "\nDNI: " + clientesTotales[CantidadClientes].DniCli +
                        "\nCUENTA: " + clientesTotales[CantidadClientes].CuentaCli);


                        //---- WINDOW VISTABANCO: CUENTAS
                        //********AGREGANDO INFORMACION DE LA CUENTA A LISTA ************
                        List_Cuentas.Items.Add(" " + clientesTotales[CantidadClientes].CuentaCli+ "   |   " + clientesTotales[CantidadClientes].DineroCuenta + "€" + "   |   " + clientesTotales[CantidadClientes].NombreCli + " | " + clientesTotales[CantidadClientes].ClientSucur);
                        //---- WINDOW VISTABANCO: CLIENTE    
                        List_Clientes.Items.Add(clientesTotales[CantidadClientes].NombreCli + " | " + clientesTotales[CantidadClientes].ApellidoCli + " | " + clientesTotales[CantidadClientes].DniCli + " | " + clientesTotales[CantidadClientes].CuentaCli + " | " + clientesTotales[CantidadClientes].ClientSucur);



                        //-----------VERIFICACIONES -----------------
                        // MAX LIMITE DE CLIENTES POR SUCURSAL:  CONTAREMOS CUANTOS CLIENTES TIENE SEGÚN SU UBICACIÓN
                        int CantidadClientesSucursal =0;
                        for (int i = 0; i < CantidadClientes+1; i++)
                        {
                            if (clientesTotales[i].Ubicacion == sucursalesTotales[Combo_Sucursales.SelectedIndex].SucursalLocation) { //EL NOMBRE DE LA CIUDAD PUEDE SER EL MISMO, "ALICANTE", PERO AMBAS TENDRÁN DIRECCIONES DIFERENTES.                              
                               CantidadClientesSucursal++;
                            }
                        }

                        // CONDICIÓN PARA LIMITES DE CLIENTES POR SUCURSAL
                        if (CantidadClientesSucursal == CantidadMaxClientes)
                        {
                            int posicion = Combo_Sucursales.SelectedIndex;
                            Combo_Sucursales.Items.RemoveAt(posicion); //ELIMINARÉ LA SUCURSAL DE LA LISTA!
                            Combo_Sucursales.Items.Insert(posicion, "---"); //Y LE CAMBIO EL NOMBRE A "---"
                            MessageBox.Show(
                         "LA SUCURSAL: " + clientesTotales[CantidadClientes].ClientSucur +
                         "\n HA LLEGADO AL LÍMITE DE CLIENTES " + (CantidadClientesSucursal));
                        }

                        CantidadClientes++;

                        //LIMPIAMOS LOS CAMPOS
                        Text_Nombre.Clear();
                        Text_Apellido.Clear();
                        Text_DNI.Clear();
                        Combo_Sucursales.SelectedIndex = -1;
                        MostrarCuenta.Visibility = Visibility.Hidden;

                        //CUANDO SE LLEGUE AL LÍMITE DE CLIENTES TOTALES CREADOS
                        if( CantidadClientes >= clientesTotales.Length)
                        {
                            //CUANDO YA ESTÉN TODAS LOS CLIENTES(100) SE BLOQUEARÁN LOS CAMPOS
                            MessageBox.Show("Has llegado al Límite Clientes");
                            Text_Nombre.IsEnabled = false;
                            Text_Apellido.IsEnabled = false;
                            Text_DNI.IsEnabled = false;
                            Combo_Sucursales.IsEnabled = false;
                        }


                    }
                    break;
            }
        }

        // WINDOW VISTA BANCO: SUCURSALES -- MOSTRAR INFORMACION DE SUCURSALES Y SUS CUENTAS
        private void Combo_Selection(object sender, SelectionChangedEventArgs e)
        {
            List_CuentaSucur.Items.Clear();
            int seleccion = Combo_Sucursales2.SelectedIndex;

            //****VISTA SUCURSALES
            if (Combo_Sucursales2.SelectedIndex != -1)
            {
                Text_SucursalInfoUbi.Text = sucursalesTotales[seleccion].SucursalLocation;
                Text_SucursalInfoCP.Text = sucursalesTotales[seleccion].SucurCP;
                Text_SucursalCod.Text = sucursalesTotales[seleccion].SucursalCod;

                //RECORRO LOS CLIENTES QUE TENGO (A CADA CLIENTE SE LE ASOCIA UNA CUENTA Y UNA SUCURSAL)
                for (int i = 0; i < CantidadClientes; i++)
                {
                    //SI LA DIRECCION DE LA SUCURSAL  DEL CLIENTE  = AL LA DIRECCION DE LA SUCURSAL SELECCIONADA
                    if (clientesTotales[i].Ubicacion == sucursalesTotales[seleccion].SucursalLocation)
                    {
                        List_CuentaSucur.Items.Add(clientesTotales[i].CuentaCli + " | " + clientesTotales[i].NombreCli);
                    }
                }


            };


        }



        // ****   BOTONES  *****
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            botones = sender as Button;


            switch (botones.Name)
            {
                //******* WINDOW: PANTALLA PRINCIPAL*****/
                case "X": Close(); break;
                case "Vista_BancoPrincipal":
                    PrincipalWindow.Visibility = Visibility.Hidden;
                    FormularioWindow1.Visibility = Visibility.Hidden;
                    VistaBancoWindow.Visibility = Visibility.Visible;
                    break;

                case "FormPrincipal":
                    PrincipalWindow.Visibility = Visibility.Hidden;
                    FormularioWindow1.Visibility = Visibility.Visible;
                    VistaBancoWindow.Visibility = Visibility.Hidden;
                    break;

                //******* WINDOW VISTABANCO : CLIENTES
                //INGRESAR Y RETIRAR DINERO
                case "Ingresar":
                    if (Text_MontoIngreso.Text == "" || Text_MontoIngreso.Text == null)
                    { MessageBox.Show("Ingrese Importe"); }
                    else
                    {
                        int nuevoMonto = clientesTotales[List_Clientes.SelectedIndex].DineroCuenta + Convert.ToInt32(Text_MontoIngreso.Text);
                        clientesTotales[List_Clientes.SelectedIndex].DineroCuenta = nuevoMonto;
                        Text_DineroIngreso.Text = clientesTotales[List_Clientes.SelectedIndex].DineroCuenta.ToString(); //MODIFICO LA CASILLA BLOQUEADA
                        List_Cuentas.Items[List_Clientes.SelectedIndex] = " " + clientesTotales[List_Clientes.SelectedIndex].CuentaCli + "   |   " + clientesTotales[List_Clientes.SelectedIndex].DineroCuenta + "€" + "   |   " + clientesTotales[List_Clientes.SelectedIndex].NombreCli + " | " + clientesTotales[List_Clientes.SelectedIndex].ClientSucur;

                    };

                    break;
                case "Retirar":
                    if (Text_MontoIngreso.Text == "" || Text_MontoIngreso.Text == null)
                    { MessageBox.Show("Ingrese Importe"); }
                    else if (clientesTotales[List_Clientes.SelectedIndex].DineroCuenta < Convert.ToInt32(Text_MontoIngreso.Text))
                    {
                        MessageBox.Show("Importe Inválido\nSólo se aceptan valores menores o iguales a tu saldo de cuenta");
                    }
                    else
                    {
                        int nuevoMonto = clientesTotales[List_Clientes.SelectedIndex].DineroCuenta - Convert.ToInt32(Text_MontoIngreso.Text);
                        clientesTotales[List_Clientes.SelectedIndex].DineroCuenta = nuevoMonto;
                        Text_DineroIngreso.Text = clientesTotales[List_Clientes.SelectedIndex].DineroCuenta.ToString(); //MODIFICO LA CASILLA BLOQUEADA
                        List_Cuentas.Items[List_Clientes.SelectedIndex] = " " + clientesTotales[List_Clientes.SelectedIndex].CuentaCli+ "   |   " + clientesTotales[List_Clientes.SelectedIndex].DineroCuenta + "€" + "   |   " + clientesTotales[List_Clientes.SelectedIndex].NombreCli + " | " + clientesTotales[List_Clientes.SelectedIndex].ClientSucur;

                    };

                    break;

                //CREAR EL IBAN
                case "IBAN":
                    string dniRemove;
                    string abc = "abcdefghijklmopqrstuvwxyzñ".ToUpper();
                    string sucursalCod = "";

                    //ELIMINANDO DELA DNI LAS LETRAS
                    dniRemove = clientesTotales[List_Clientes.SelectedIndex].DniCli;
                    dniRemove = new string(dniRemove.Where(c => !abc.Contains(c)).ToArray());

                    //PARA BUSCAR EL CODIGO DE LA SUCURSAL
                    for (int i = 0; i < CantidadSucursal ; i++)
                    {
                        // cuando el nombre de la sucursal de la cuenta sea == al de la sucursal
                        if (clientesTotales[List_Clientes.SelectedIndex].Ubicacion == sucursalesTotales[i].SucursalLocation)
                        {   sucursalCod = sucursalesTotales[i].SucursalCod; break; }

                    };

                    Text_Iban.Text = (sucursalCod + " " + clientesTotales[List_Clientes.SelectedIndex].CuentaCli + " " + dniRemove);


                    break;

            };


        }
        // ****   BOTONES DEL MENUITEM EN EL NAVEGADOR  *****
        private void menu_click(object sender, RoutedEventArgs e)
        {

            menuItem = sender as MenuItem;

            switch (menuItem.Name)
            {
                case "menuVista":
                    FormularioWindow1.Visibility = Visibility.Hidden;
                    VistaBancoWindow.Visibility = Visibility.Visible;
                    break;

                case "menuForm":
                    FormularioWindow1.Visibility = Visibility.Visible;
                    VistaBancoWindow.Visibility = Visibility.Hidden;
                    break;

            }

        }

        //VISTA BANCO: CLIENTES
        // SE UTILIZA CUANDO SE SELECIONA UN CLIENTE Y QUEREMOS OPERAR EN SU CUENTA (INGRESO O RETIRAR DINERO)
        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Text_MontoIngreso.Clear();
            Text_Iban.Clear();
            List_Clientes.SelectedIndex = List_Clientes.SelectedIndex; //CON ESTO SABRÉ EL NUMERO DE LA LISTA SELECCIONADO
            Text_ClienteIngreso.Text = clientesTotales[List_Clientes.SelectedIndex].NombreCli + " " + clientesTotales[List_Clientes.SelectedIndex].ApellidoCli;
            Text_CodigoIngreso.Text = clientesTotales[List_Clientes.SelectedIndex].CuentaCli;
            Text_DineroIngreso.Text = clientesTotales[List_Clientes.SelectedIndex].DineroCuenta.ToString();
        }

    } 
}

