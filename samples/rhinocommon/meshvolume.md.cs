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
public class MeshVolumeCommand : Command
{
  public override string EnglishName { get { return "csMeshVolume"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var gm = new GetObject();
    gm.SetCommandPrompt("Select solid meshes for volume calculation");
    gm.GeometryFilter = ObjectType.Mesh;
    gm.GeometryAttributeFilter = GeometryAttributeFilter.ClosedMesh;
    gm.SubObjectSelect = false;
    gm.GroupSelect = true;
    gm.GetMultiple(1, 0);
    if (gm.CommandResult() != Result.Success)
      return gm.CommandResult();

    double volume = 0.0;
    double volume_error = 0.0;
    foreach (var obj_ref in gm.Objects())
    {
      if (obj_ref.Mesh() != null)
      {
        var mass_properties = VolumeMassProperties.Compute(obj_ref.Mesh());
        if (mass_properties != null)
        {
          volume += mass_properties.Volume;
          volume_error += mass_properties.VolumeError;
        }
      }
    }

    RhinoApp.WriteLine("Total volume = {0:f} (+/- {1:f})", volume, volume_error);
    return Result.Success;
  }
}
}
