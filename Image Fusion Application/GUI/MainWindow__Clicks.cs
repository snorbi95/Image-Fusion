using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Fusion_Application
{
    public partial class MainWindow
    {
        //irb fajl betoltese
        private void loadIRB_Click(object sender, EventArgs e)
        {
            FusedImage.Image = null;
            readInfraredImage();
            readVisibleImage();
        }

        //hokep teljes kepernyos uzemmod
        private void ThermalImage_Click(object sender, EventArgs e)
        {
            Form thermalImageWindow = new Form();
            PictureBox pb = new PictureBox();
            pb.Image = ThermalImage.Image;
            pb.Dock = DockStyle.Fill;
            thermalImageWindow.Controls.Add(pb);
            thermalImageWindow.Size = new Size(1024, 768);
            thermalImageWindow.ShowDialog();
        }

        //rgb kep teljes kepernyos uzemmod
        private void VisibleImage_Click(object sender, EventArgs e)
        {
            Form fusedImageWindow = new Form();
            PictureBox pb = new PictureBox();
            pb.Image = VisibleImage.Image;
            pb.Dock = DockStyle.Fill;
            fusedImageWindow.Controls.Add(pb);
            fusedImageWindow.Size = new Size(1024, 768);
            fusedImageWindow.ShowDialog();
        }

        private void fusionMethods_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < fusionMethods.Items.Count; ++ix)
                if (ix != e.Index) fusionMethods.SetItemChecked(ix, false);
        }

        private string checkFusionMethod() {
            return fusionMethods.CheckedItems[0].ToString();
        }

        //kepfuzionalo program lefuttatasa
        private void fuseImages_Click(object sender, EventArgs e)
        {
            //kepek kimentese
            ThermalImage.Image.Save(@"thermal\" + fileName + "_.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            VisibleImage.Image.Save(@"visible\" + fileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            maskImage.Image.Save(@"thermal\" + fileName + "_mask.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            processOngoing.Text = "Fusing images...";
            processOngoing.Refresh();


            var method = checkFusionMethod();

            var argument = 0.0;

            switch (method) {
                case "Replace":
                    argument = thresholdScroll.Value / 100.0;
                    break;
                case "Saliency":
                    argument = ratioScroll.Value / 100.0;
                    break;
            }
            var script = @"image_fusion.py";
            var command = $"python \"{script}\" \"{fileName}\" \"{bmpScaleX}\" \"{bmpScaleY}\" \"{method}\" \"{argument}\"";
            var psi = new ProcessStartInfo("cmd","/c " + command);
            psi.Verb = "runas";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";

            var results = "";
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Console.WriteLine(errors);
            Console.WriteLine(results);
            processOngoing.Text = "";
            processOngoing.Refresh();
            FusedImage.Image = Image.FromFile(@"results\final_image_" + fileName + ".png");
        }

        //fuzionalt kep teljes kepernyos uzemmod
        private void FusedImage_Click(object sender, EventArgs e)
        {
            Form fusedImageWindow = new Form();
            PictureBox pb = new PictureBox();
            pb.Image = FusedImage.Image;
            pb.Dock = DockStyle.Fill;
            fusedImageWindow.Controls.Add(pb);
            fusedImageWindow.Size = new Size(1024, 768);
            fusedImageWindow.ShowDialog();
        }

        //hokep atfestese
        private void repaintThermalImage_Click(object sender, EventArgs e)
        {
            ShowNextFrame();
        }

        //irb fajl kitallozasa
        private void getFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openIRB = new OpenFileDialog
            {
                Title = "Browse IRB File",
                DefaultExt = "irb"
            };

            if (openIRB.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = openIRB.FileName;
            }
        }

        private void thresholdScroll_ValueChanged(object sender, EventArgs e)
        {
            var value = thresholdScroll.Value / 100.0;
            thresholdText.Text = value.ToString();
        }

        private void ratioScroll_ValueChanged(object sender, EventArgs e)
        {
            var value = ratioScroll.Value / 100.0;
            ratioText.Text = value.ToString();
        }

        private void visibleFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"visible\");
        }

        private void thermalFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"thermal\");
        }

        private void fusedFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"results\");
        }

    }
}
