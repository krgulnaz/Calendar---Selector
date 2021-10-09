using System.Windows.Forms.Design;

namespace NaitonControls
{
  public class FixedHeightUserControlDesigner : ParentControlDesigner
  {
    private static string[] _propsToRemove = new string[] { "Height"};

    public override SelectionRules SelectionRules
    {
      get { return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable; }
    }

    protected override void PreFilterProperties(System.Collections.IDictionary properties)
    {
      base.PreFilterProperties(properties);
      foreach (string p in _propsToRemove)
        if (properties.Contains(p))
          properties.Remove(p);
    }
  }
}
