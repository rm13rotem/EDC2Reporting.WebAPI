using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Components
{
    public class ComponentFilter
    {
        public string PartOfName { get; set; }
        public string PartOfLabel { get; set; }
        public ComponentType? ComponentType { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public IEnumerable<AbstractComponent> GetCurrentList(IEnumerable<AbstractComponent> listOfComponents)
        {
            if (PageSize == 0) PageSize = 20;
            if (CurrentPage == 0) CurrentPage = 1;
            var current = listOfComponents;
            if (!string.IsNullOrWhiteSpace(PartOfName))
                current = current.Where(x => x.Name.Contains(PartOfName));
            if (!string.IsNullOrWhiteSpace(PartOfLabel))
                current = current.Where(x => x.Label.Contains(PartOfLabel));
            if (ComponentType != 0)
                current = current.Where(x => x.ComponentType == ComponentType);

            if (current.Count() > CurrentPage * PageSize)
                current = current.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            
            return current;
        }

        public static  SelectList GetSelectList(ComponentType? componentType)
        {
            var options = typeof(ComponentType).GetEnumValues();

            return new SelectList(options, componentType);
        }
    }
}
