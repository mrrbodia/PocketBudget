using Business.DomainModel.Active;
using Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Actives
{
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    public class ActiveAggregate : Aggregate
    {
        private List<IActive> items = new List<IActive>();

        public override Iterator CreateIterator()
        {
            return new ActiveIterator(this);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public IActive this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }
}
