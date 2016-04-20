using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rhino;
using Rhino.Collections;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Rhino.Geometry.Intersect;
using Rhino.Geometry.Morphs;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;
using Rhino.UI.Gumball;
using System.Runtime.InteropServices;

class TestCompileWrapperClass
{
public class CaptureViewToBitmapCommand : Rhino.Commands.Command
{
  public override string EnglishName
  {
    get { return "csCaptureViewToBitmap"; }
  }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var file_name = "";

    var bitmap = doc.Views.ActiveView.CaptureToBitmap(true, true, true);
    bitmap.MakeTransparent();

    // copy bitmap to clipboard
    Clipboard.SetImage(bitmap);

    // save bitmap to file
    var save_file_dialog = new Rhino.UI.SaveFileDialog
    {
      Filter = "*.bmp",
      InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    };
    if (save_file_dialog.ShowDialog() == DialogResult.OK)
    {
      file_name = save_file_dialog.FileName;
    }

    if (file_name != "")
      bitmap.Save(file_name);

    return Rhino.Commands.Result.Success;
  }
}
}
