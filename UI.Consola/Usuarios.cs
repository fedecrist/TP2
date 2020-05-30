using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Business.Entities;
using Business.Logic;
using static System.Console;

namespace UI.Consola
{
    public class Usuarios
    {
        Business.Logic.UsuarioLogic UsuarioNegocio { get; set; }
        public Usuarios()
        {
            this.UsuarioNegocio = new Business.Logic.UsuarioLogic();
        }

        public void Menu()
        {
            int opc = 0;
            while (opc != 6)
            {
                WriteLine("1- Listado General");
                WriteLine("2- Consulta");
                WriteLine("3- Agregar");
                WriteLine("4- Modificar");
                WriteLine("5- Eliminar");
                WriteLine("6- Salir");

                opc = int.Parse(ReadLine());
                Clear();
                switch (opc)
                {
                    case 1: ListadoGeneral(); break;
                    case 2: WriteLine("Ingrese ID del usuario a buscar");
                            Consulta(); break;
                    case 3: Agregar(); break;
                    case 4: WriteLine("Ingrese ID del usuario a modificar");
                            Modificar(); break;
                    case 5: WriteLine("Ingrese ID del usuario que desea eliminar");
                            Eliminar(); break;
                    case 6: break;
                }
                WriteLine("Presione una tecla para continuar"); ReadKey();
                Clear();
            }

        }

        #region ListadoGeneral y MostrarDatos
        public void MostrarDatos(Usuario usr)
        {
            WriteLine("Usuario: {0}", usr.ID);
            WriteLine("Nombre: {0}", usr.Nombre);
            WriteLine("Apellido: {0}", usr.Apellido);
            WriteLine("Nombre de usuario: {0}", usr.NombreUsuario);
            WriteLine("Clave: {0}", usr.Clave);
            WriteLine("Email: {0}", usr.Email);
            WriteLine();
        }

        public void ListadoGeneral()
        {
            Clear();
            foreach (Usuario usr in UsuarioNegocio.GetAll())
            {
                MostrarDatos(usr);
            }
        }

        #endregion

        #region Consultar
        public void Consulta ()
        {
            try
            {
                MostrarDatos(UsuarioNegocio.GetOne(int.Parse(ReadLine())));
            }
            catch (FormatException fe)
            {
                WriteLine();
                WriteLine("La ID ingresada debe ser un número entero");
            }
            catch (Exception e)
            {
                WriteLine();
                WriteLine(e.Message);
            }
            finally
            {
                WriteLine("Presione una tecla para continuar");
                ReadKey();
            }
        }
        #endregion

        #region Agregar
        public void Agregar()
        {
            Usuario usr = new Usuario();
            WriteLine("Ingrese su nombre"); usr.Nombre = ReadLine();
            WriteLine("Ingrese su apellido)"); usr.Apellido = ReadLine();
            WriteLine("Ingrese su nombre de usuario"); usr.NombreUsuario = ReadLine();
            WriteLine("Ingrese su clave"); usr.Clave = ReadLine();
            WriteLine("Ingrese su email"); usr.Email = ReadLine();
            WriteLine("Ingrese habilitación de usuario (1- Sí/Otro- No)"); usr.Habilitado = (ReadLine() == "1");
            usr.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usr);
            WriteLine("ID de usuario: {0}", usr.ID);
        }
        #endregion

        #region Modificar
        public void Modificar()
        {
            try
            {
                Usuario usr = UsuarioNegocio.GetOne(int.Parse(ReadLine()));
                MostrarDatos(usr);
                int opc1 = 0;
                while (opc1 != 6)
                {
                    WriteLine("¿Qué desea modificar?");
                    WriteLine("1- Nombre");
                    WriteLine("2- Apellido");
                    WriteLine("3- Nombre de usuario");
                    WriteLine("4- Clave");
                    WriteLine("5- Email");
                    WriteLine("6- Habilitación");
                    WriteLine("7- Salir");
                    opc1 = int.Parse(ReadLine());
                    Clear();
                    switch (opc1)
                    {
                        case 1: WriteLine("Ingrese el nuevo nombre"); usr.Nombre = ReadLine(); break;
                        case 2: WriteLine("Ingrese el nuevo apellido"); usr.Apellido = ReadLine(); break;
                        case 3: WriteLine("Ingrese el nuevo nombre de usuario"); usr.NombreUsuario = ReadLine(); break;
                        case 4: WriteLine("Ingrese la nueva clave"); usr.Clave = ReadLine(); break;
                        case 5: WriteLine("Ingrese el nuevo email"); usr.Email = ReadLine(); break;
                        case 6: WriteLine("Ingrese habilitación de usuario |1- Si/otro- No"); usr.Habilitado =  ReadLine() == "1" ; break;
                        case 7: break;
                    }
                    usr.State = BusinessEntity.States.New;
                    UsuarioNegocio.Save(usr);
                    Clear();
                }
            }
            catch (FormatException fe)
            {
                WriteLine();
                WriteLine("La ID debe ser un numero entero");
            }
            catch (Exception e)
            {
                WriteLine();
                WriteLine(e);
            }
            finally
            {
                WriteLine("Ingrese una tecla para continuar");
                ReadKey();
            }
            
        }
        #endregion

        #region Eliminar
        public void Eliminar()
        {
            try
            {
                UsuarioNegocio.Delete(int.Parse(ReadLine()));
            }
            catch (FormatException fe)
            {
                WriteLine();
                WriteLine("El ID ingresado debe ser un entero");
            }
            catch (Exception e)
            {
                WriteLine();
                WriteLine(e);
            }
            finally
            {
                WriteLine("Ingrese una letra para continuar");
                ReadKey();
            }
        }
        #endregion
    }
}
