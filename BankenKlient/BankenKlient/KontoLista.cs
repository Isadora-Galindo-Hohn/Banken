using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    /*
     * Konto listen 
     */
    class Lista<T>
    {
        T[] items;

        public Lista()
        {
            items = new T[0];
        }

        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set 
            {
                items[index] = value;
            }
        }

        public void Add(T newItem)
        {
            T[] newItems = new T[items.Length + 1];

            for (int index = 0; index < items.Length; index++)
            {
                newItems[index] = items[index];
            }

            newItems[newItems.Length - 1] = newItem;

            items = newItems;
        }

        public void Remove(int index)
        {
            T[] newItems = new T[items.Length - 1];


            items = newItems;
        }

        public int Length()
        {
            return items.Length;
        }

    }
}

