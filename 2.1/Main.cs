using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Selection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application; //Заход в преложение ревит
            UIDocument uidoc = uiapp.ActiveUIDocument; //обращение к классу (завбираем интерфейс текущего документа
            Document doc = uidoc.Document; //обращение к документу
            
            List<FamilyInstance> familyList = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_DuctSystem)
            .WhereElementIsNotElementType()
            .Cast<FamilyInstance>()
            .ToList();

            TaskDialog.Show("Duct count", familyList.Count.ToString());
            return Result.Succeeded;
        }
    }
}
