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
            //Множественный выбор элементов
            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберете стены");
            var wallList = new List<Wall>();

            string info = string.Empty;

            foreach (var selectedElement in selectedElementRefList)
            {
                Wall oWall = doc.GetElement(selectedElement) as Wall;
                wallList.Add(oWall);
                var width = UnitUtils.ConvertFromInternalUnits(oWall.Width, UnitTypeId.Millimeters);
                info += $"Mame: {oWall.Name}, width: {width}";
            }
            info += $"Количество: {wallList.Count}";
            //Выбор одного элемента
            // Reference selectedElementRef = uidoc.Selection.PickObjects(ObjectType.Element, "Выберете элементы"); //Element-сам элемент Face-грань Edge-ребро выбираем элемент
            // Element element = doc.GetElement(selectedElementRef);
            //TaskDialog.Show("Selection", $"Имя: {element.Name}{Environment.NewLine}Id: {element.Id}"); //информация о элементе в диалоговом окне
            TaskDialog.Show("Selection", info);
            return Result.Succeeded;
        }
    }
}
