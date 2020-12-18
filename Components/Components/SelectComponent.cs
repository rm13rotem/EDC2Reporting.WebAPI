using System;

namespace Components
{
    public class SelectComponent : AbstractComponent
    {
        public int CategoryId { get; set; }
        public int SelectedValue { get; set; }

        public SelectComponent(string label, string name, int _categoryId, int defaultValue = 0 /*Not Selected*/)
            : base(label, name)
        {
            CategoryId = _categoryId;
        }
    }
}
