using Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Actives
{
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract object IsDone();
        public abstract object CurrentItem();
    }

    public class ActiveIterator : Iterator
    {
        private ActiveAggregate aggregate;
        private int current;

        public ActiveIterator(ActiveAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public override object First()
        {
            return aggregate[0];
        }

        public override object Next()
        {
            object next = null;
            if (current < aggregate.Count - 1)
            {
                next = aggregate[++current];
            }
            return next;
        }

        public override object CurrentItem()
        {
            return aggregate[current];
        }

        public override object IsDone()
        {
            return current >= aggregate.Count;
        }   
    }
}
