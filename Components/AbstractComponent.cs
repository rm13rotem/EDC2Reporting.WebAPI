namespace Components
{
    public class AbstractComponent
    {
        public string Name { get; private set; }
        public string Label { get; private set; }

        public AbstractComponent(string label, string name)
        {
            Label = label;
            this.Name = name;
        }

        public int OrderId { get; set; } // seriale order inside a module/form 1,2,3...
        public virtual string SerializedValue { get; set; }
        public virtual string DefaultValue { get; set; }

    }
}