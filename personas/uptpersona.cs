using personas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace personas
{
    public partial class uptpersona : Form
    {
        public int? id = null;
        persona opersona = null;
        public uptpersona(int? id)
        {
            InitializeComponent();
            this.id = id;

            if (id.HasValue) loadData();
        }

        private void loadData()
        {
            using (personasEntities db = new personasEntities())
            {
                opersona = db.persona.Find(id);
                txtuptname.Text = opersona.nombre;
                txtuptapellido.Text = opersona.apellido;
            }
            

        }
        private void uptpersona_Load(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            using(personasEntities db = new personasEntities())
            {
                if (id.HasValue)
                {
                    opersona.nombre = txtuptname.Text;
                    opersona.apellido = txtuptapellido.Text;
                }
                db.Entry(opersona).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
