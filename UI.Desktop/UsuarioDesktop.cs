using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;
using System.Text.RegularExpressions;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public UsuarioDesktop()
        {
            InitializeComponent();
        }
        public Usuario UsuarioActual { get; set; }

        public UsuarioDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }
        public UsuarioDesktop(int ID, ModoForm modo) : this(modo)
        {
            UsuarioLogic ul = new UsuarioLogic();
            UsuarioActual = ul.GetOne(ID);
            MapearDeDatos();
        }
        
        public override void MapearDeDatos()
        /*  va a ser utilizado en cada formulario para copiar la
        información de las entidades a los controles del formulario(TextBox,
        ComboBox, etc) para mostrar la infromación de cada entidad */
        {
            this.lblID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.lblNombre.Text = this.UsuarioActual.Nombre;
            this.lblApellido.Text = this.UsuarioActual.Apellido;
            this.lblClave.Text = this.UsuarioActual.Clave;
            this.lblUsuario.Text = this.UsuarioActual.NombreUsuario;
            this.btnAceptar.Text = this.Modo.ToString();
            switch (this.Modo)
            {
                case ModoForm.Alta:
                case ModoForm.Modificacion:
            {
                btnAceptar.Text = "Guardar";
                break;
            }
                case ModoForm.Baja:
                    {
                        btnAceptar.Text = "Eliminar";
                        break;
                    }
                default:
                    {
                        btnAceptar.Text = "Aceptar";
                        break;
                    } 
            }
        }
        public override void MapearADatos()
        /*se va a utilizar para pasar la información de los
        controles a una entidad para luego enviarla a las capas inferiores */
        {
            if (this.Modo == ModoForm.Alta || this.Modo == ModoForm.Modificacion)
            {
                if (this.Modo == ModoForm.Alta) this.UsuarioActual = new Usuario();
                else this.UsuarioActual.ID = int.Parse(this.txtID.Text);

                this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                this.UsuarioActual.Nombre = this.txtNombre.Text;
                this.UsuarioActual.Apellido = this.txtApellido.Text;
                this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                this.UsuarioActual.Clave = this.txtClave.Text;
                this.UsuarioActual.Email = this.txtEmail.Text;
            }
            switch (Modo)
            {
                case ModoForm.Alta: UsuarioActual.State = BusinessEntity.States.New; break;
                case ModoForm.Baja: UsuarioActual.State = BusinessEntity.States.Deleted; break;
                case ModoForm.Consulta: UsuarioActual.State = BusinessEntity.States.Unmodified; break;
                case ModoForm.Modificacion: UsuarioActual.State = BusinessEntity.States.Modified; break;
            }
        }
        public override void GuardarCambios()
        /*es el método que se encargará de invocar al método
        correspondiente de la capa de negocio según sea el ModoForm en que se encuentre el formulario*/
        {
           MapearADatos();
                switch (this.Modo)
                {
                    case ModoForm.Alta:
                    case ModoForm.Modificacion:
                        {
                            new UsuarioLogic().Save(UsuarioActual);
                            break;
                        }
                    case ModoForm.Baja:
                        {
                            new UsuarioLogic().Delete(UsuarioActual.ID);
                            break;
                        }
                    default: break;
                }
            }
        public override bool Validar()
        /*será el método que devuelva si los datos son válidos para poder registrar los cambios realizados. */
        { if (txtClave.Text.Length >= 8 && txtClave.Text == txtConfirmarClave.Text && 
                email_bien_escrito(txtEmail.Text)==true && !string.IsNullOrEmpty(txtNombre.Text) &&
                !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtUsuario.Text))
            {
                return true;
            }
          else
            {
                Notificar("Algún campo está mal cargado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar()) 
            { 
                this.GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
