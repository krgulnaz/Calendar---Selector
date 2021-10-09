using System.ComponentModel;
using System.Windows.Forms;

namespace NaitonControls
{
  [ToolboxItem(false)]
  public class DCButton : Button
  {
    public DCButton()
    {
      SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
    }
  }
}
