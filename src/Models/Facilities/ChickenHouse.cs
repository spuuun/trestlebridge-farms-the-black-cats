using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Actions;


namespace Trestlebridge.Models.Facilities
{
    public class ChickenHouse : IFacility<IChicken>
    {
        private int _capacity = 15;
        private Guid _id = Guid.NewGuid();

        private List<IChicken> _chickens = new List<IChicken>();

        public double Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public List<IChicken> Chickens
        {
            get
            {
                return _chickens;
            }
        }

        public void AddResource(IChicken chicken)
        {
            _chickens.Add(chicken);
        }

        public void AddResource(List<IChicken> chickens)
        {
            // TODO: implement this...
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";

            output.Append($"Chicken House {shortId} has {this._chickens.Count} chickens\n");
            this._chickens.ForEach(a => output.Append($"   {a}\n"));

            return output.ToString();
        }
    }
}