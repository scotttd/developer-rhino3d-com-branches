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
/// <summary>
/// Generate a layout with a single detail view that zooms to a list of objects
/// </summary>
/// <param name="doc"></param>
/// <returns></returns>
public static Rhino.Commands.Result AddLayout(Rhino.RhinoDoc doc)
{
  doc.PageUnitSystem = Rhino.UnitSystem.Millimeters;
  var page_views = doc.Views.GetPageViews();
  int page_number = (page_views==null) ? 1 : page_views.Length + 1;
  var pageview = doc.Views.AddPageView(string.Format("A0_{0}",page_number), 1189, 841);
  if( pageview!=null )
  {
    Rhino.Geometry.Point2d top_left = new Rhino.Geometry.Point2d(20,821);
    Rhino.Geometry.Point2d bottom_right = new Rhino.Geometry.Point2d(1169, 20);
    var detail = pageview.AddDetailView("ModelView", top_left, bottom_right, Rhino.Display.DefinedViewportProjection.Top);
    if (detail != null)
    {
      pageview.SetActiveDetail(detail.Id);
      detail.Viewport.ZoomExtents();
      detail.DetailGeometry.IsProjectionLocked = true;
      detail.DetailGeometry.SetScale(1, doc.ModelUnitSystem, 10, doc.PageUnitSystem);
      // Commit changes tells the document to replace the document's detail object
      // with the modified one that we just adjusted
      detail.CommitChanges();
    }
    pageview.SetPageAsActive();
    doc.Views.ActiveView = pageview;
    doc.Views.Redraw();
    return Rhino.Commands.Result.Success;
  }
  return Rhino.Commands.Result.Failure;
}
}
