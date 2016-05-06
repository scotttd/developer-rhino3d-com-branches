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
public class ExtractThumbnailCommand : Command
{
  public override string EnglishName { get { return "csExtractThumbnail"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var gf = RhinoGet.GetFileName(GetFileNameMode.OpenImage, "*.3dm", "select file", null);
    if (gf == string.Empty || !System.IO.File.Exists(gf))
      return Result.Cancel;

    var bitmap = Rhino.FileIO.File3dm.ReadPreviewImage(gf);
    // convert System.Drawing.Bitmap to BitmapSource
    var image_source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
      Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

    // show in WPF window
    var window = new Window();
    var image = new Image {Source = image_source};
    window.Content = image;
    window.Show();

    return Result.Success;
  }
}
}
