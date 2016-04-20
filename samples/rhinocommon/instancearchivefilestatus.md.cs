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
public static Rhino.Commands.Result InstanceArchiveFileStatus(Rhino.RhinoDoc doc)
{
  for (int i = 0; i < doc.InstanceDefinitions.Count; i++)
  {
    Rhino.DocObjects.InstanceDefinition iDef = doc.InstanceDefinitions[i];
    Rhino.DocObjects.InstanceDefinitionArchiveFileStatus iDefStatus = iDef.ArchiveFileStatus;

    string status = "Unknown";
    switch (iDefStatus)
    {
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.NotALinkedInstanceDefinition:
        status = "not a linked instance definition.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileNotReadable:
        status = "archive file is not readable.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileNotFound:
        status = "archive file cannot be found.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileIsUpToDate:
        status = "archive file is up-to-date.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileIsNewer:
        status = "archive file is newer.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileIsOlder:
        status = "archive file is older.";
        break;
      case Rhino.DocObjects.InstanceDefinitionArchiveFileStatus.LinkedFileIsDifferent:
        status = "archive file is different.";
        break;
    }

    Rhino.RhinoApp.WriteLine("{0} - {1}", iDef.Name, status);
  }

  return Rhino.Commands.Result.Success;
}
}
