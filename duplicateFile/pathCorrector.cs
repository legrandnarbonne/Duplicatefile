using duplicateFile.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace duplicateFile
{
    public partial class PathCorrector : Form
    {
        public DataTable Paths;
        private readonly DataTable _config = new DataTable("Correspondance");
        private readonly string _exePath = AppDomain.CurrentDomain.BaseDirectory;

        public PathCorrector()
        {
            InitializeComponent();
            _config.Columns.Add(Resources.Languages.Resources.Txt_Caracteres, typeof(string));
            _config.Columns.Add(Resources.Languages.Resources.Txt_Remplacer_Par, typeof(string));

            var fs = new FileStream(_exePath + "\\correspondances.conf", FileMode.Open);
            _config.ReadXml(fs);
            fs.Close();

            _config.AcceptChanges();

            dgvConv.DataSource = _config;
        }

        private void pathCorrector_Load(object sender, EventArgs e)
        {
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            SaveConf();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SaveConf()
        {
            var dtCh = _config.GetChanges();

            if (dtCh == null) return;
            if (MessageBox.Show(
                Resources.Languages.Resources.Txt_La_table_de_correspondance_a_étée_modifiée_souhaitez_vous_enregistrer_les_modifications_,
                Resources.Languages.Resources.Txt_Confirmation, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            var fs = new FileStream(_exePath + "\\correspondances.conf", FileMode.Create);
            _config.WriteXml(fs);
            fs.Close();
        }

        private void btnCorriger_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Corrige() + Resources.Languages.Resources.Txt_correction_s__effectuée_s__,
                Resources.Languages.Resources.Txt_Information, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private int Corrige()
        {
            var cmpt = 0;

            var drtc = new List<DataRow>();
            var nPath = new List<string>();

            foreach (DataRow dr in Paths.Rows)
            {
                var path = dr["Fichier"].ToString();
                var fileName = Path.GetFileNameWithoutExtension(path);
                var directoryName = Path.GetDirectoryName(path);
                var ext = Path.GetExtension(path);

                var ignore = false;

                for (var a = 0; a < _config.Rows.Count; a++)
                {
                    var dc = _config.Rows[a];
                    fileName = fileName.Replace(
                        dc[Resources.Languages.Resources.Txt_Caracteres].ToString(),
                        dc[Resources.Languages.Resources.Txt_Remplacer_Par].ToString());
                }
                var newPath = directoryName + "\\" + fileName + ext;
                if (path == newPath) continue;
                if (chkConfirm.Checked)
                {
                    var d = MessageBox.Show(
                        Resources.Languages.Resources.Txt_Confirmez_vous_la_correction + path + Resources.Languages.Resources.Txt_En + newPath, Resources.Languages.Resources.Txt_Confirmation,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (d)
                    {
                        case DialogResult.Cancel:
                            return cmpt;

                        case DialogResult.No:
                            ignore = true;
                            break;
                    }
                }

                if (ignore) continue;
                cmpt++;
                var lastPath = "";
                if (renameFile(path, newPath, out lastPath))
                {
                    return cmpt; //Abort
                }
                if (lastPath != newPath) continue;
                drtc.Add(dr);
                nPath.Add(newPath);
            }

            for (var a = 0; a < drtc.Count; a++)
            {
                drtc[a]["Fichier"] = nPath[a];
            }

            return cmpt;
        }

        private bool renameFile(string path, string newPath, out string lastPath)
        {
            try
            {
                var fi = new FileInfo(path);
                fi.MoveTo(newPath);
                lastPath = newPath;
            }
            catch (Exception)
            {
                lastPath = path;
                var d = MessageBox.Show(Resources.Languages.Resources.Error_pendant_le_renomage + path + Resources.Languages.Resources.Txt_En + newPath, Resources.Languages.Resources.Erreur,
                    MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (d == DialogResult.Retry) return renameFile(path, newPath, out lastPath);
                if (d == DialogResult.Ignore) return false;
                if (d == DialogResult.Abort) return true;
            }

            return false;
        }
    }
}