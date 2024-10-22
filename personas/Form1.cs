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
    public partial class Personas : Form
    {
        public Personas()
        {
            InitializeComponent();
        }

        private void Personas_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            using (personasEntities db = new personasEntities())
            {
                var list = from d in db.persona select d;

                dataGridView1.DataSource = list.ToList();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            using(personasEntities db = new personasEntities())
            {
                persona opersona = new persona();
                opersona.nombre = txtname.Text;
                opersona.apellido = txtapellido.Text;

                db.persona.Add(opersona);
                db.SaveChanges();
                refresh();
                txtname.Clear();
                txtapellido.Clear();
                
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            int? id = getID();

            if (id.HasValue)
            {
                uptpersona update = new uptpersona(id);
                update.ShowDialog();
                refresh();
            }
        }

        public int? getID()
        {
            return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            int? id = getID();

            if (id.HasValue)
            {
                using (personasEntities db = new personasEntities())
                {
                    persona opersona = db.persona.Find(id);
                    db.persona.Remove(opersona);

                    db.SaveChanges();
                    refresh();
                }
            }
        }
    }
}
